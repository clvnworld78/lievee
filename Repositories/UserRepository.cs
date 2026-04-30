using lievee.Models;
using Npgsql;

namespace lievee.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Database.Database _db;
        public UserRepository(Database.Database db) { _db = db; }

        public async Task<long> SaveNewUserAsync(string username, byte[] password, UserRole role)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            using var tx = await dbConn.BeginTransactionAsync();

            try
            {
                var query = "INSERT INTO users (username, password, role) VALUES ($1, $2, $3::user_role) RETURNING user_id";
                using var sql = new NpgsqlCommand(query, dbConn);
                sql.Parameters.Add(new NpgsqlParameter { Value = username });
                sql.Parameters.Add(new NpgsqlParameter { Value = password });
                sql.Parameters.Add(new NpgsqlParameter { Value = role.ToString() });

                var result = await sql.ExecuteScalarAsync();
                if (result == null)
                {
                    return 0;
                }

                await tx.CommitAsync();
                return Convert.ToInt64(result);
            } catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
