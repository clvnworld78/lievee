using lievee.Models;
using Npgsql;
using System.Data;

namespace lievee.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly Database.Database _db;
        public SessionRepository(Database.Database db) { _db = db; }

        public async Task<Users> GetUserAsync(Guid token)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();

            var query = """
                SELECT s.user_id, u.username, u.password, u.role, s.expired_at
                FROM sessions s
                JOIN users u ON s.user_id = u.user_id
                WHERE token = $1
            """;
            using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = token });
            using var result = await sql.ExecuteReaderAsync();

            if (result.Read())
            {
                var userId = result.GetInt64(0);
                var username = result.GetString(1);
                var pass = result.GetFieldValue<byte[]>(2);
                var role = result.GetString(3);
                var expiredAt = result.GetDateTime(4);

                if (expiredAt > DateTime.Now)
                {
                    return Users.NewInvalidUser();
                }

                if (!Enum.TryParse<UserRole>(role, true, out var checkedRole))
                {
                    throw new Exception($"database return unknown role: {checkedRole}");
                } else
                {
                    return Users.NewAuthenticatedUser(userId, username, pass, checkedRole);
                }

            } else
            {
                // no token found
                return Users.NewInvalidUser();
            }
        }

        public async Task<Users> GetUserCredentialAsync(string username)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();

            var query = "SELECT user_id, password, role FROM users WHERE username = $1";
            using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = username });
            
            using var reader = await sql.ExecuteReaderAsync();
            if (!reader.Read())
            {
                return Users.NewInvalidUser();
            }

            var id = reader.GetInt64(0);
            var pass = reader.GetFieldValue<byte[]>(1);
            var role = reader.GetString(2);

            bool isValidRole = ParseEnum(role);
            if (!isValidRole)
            {
                throw new Exception($"database return invalid role string for username {username}");
            }

            var validRole = Enum.Parse<UserRole>(role, true);
            var user = Users.NewLogin(id, username, pass, validRole);

            return user;
        }

        public async Task SaveToken(Guid token, long userId)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            using var tx = await dbConn.BeginTransactionAsync();

            try
            {
                var query = "INSERT INTO sessions (token, user_id) VALUES ($1, $2)";
                using var sql = new NpgsqlCommand(query, dbConn);
                sql.Parameters.Add(new NpgsqlParameter { Value = token });
                sql.Parameters.Add(new NpgsqlParameter { Value = userId });
                await sql.ExecuteNonQueryAsync();

                await tx.CommitAsync();
            } catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        private bool ParseEnum(string val)
        {
            return Enum.TryParse<UserRole>(val, true, out _);
        }
    }
}
