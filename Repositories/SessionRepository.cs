using lievee.Models;
using Npgsql;

namespace lievee.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly Database.Database _db;
        public SessionRepository(Database.Database db) { _db = db; }

        public async Task<Users> GetUserAsync(string token)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();

            var query = """
                SELECT (s.user_id, u.username, u.role, expired_at)
                FROM sessions s
                JOIN users u ON s.user_id = u.user_id
                WHERE token = $1
            """;
            using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = token });
            using var result = await sql.ExecuteReaderAsync();

            if (result.Read())
            {
                var userId = result.GetInt32(0);
                var username = result.GetString(1);
                var role = result.GetString(2);
                var expiredAt = result.GetDateTime(3);

                if (expiredAt > DateTime.Now)
                {
                    return Users.NewInvalidUser();
                }

                if (!Enum.TryParse<UserRole>(role, true, out var checkedRole))
                {
                    throw new Exception($"database return unknown role: {checkedRole}");
                } else
                {
                    return Users.NewAuthenticatedUser(userId, username, checkedRole);
                }
            } else
            {
                // no token found
                return Users.NewInvalidUser();
            }
        }
    }
}
