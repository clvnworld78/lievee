using lievee.Models;
using Npgsql;
using System.Threading.Tasks;

namespace lievee.Repositories
{
    public class UniqueCodeRepository : IUniqueCodeRepository
    {
        private readonly Database.Database _db;
        public UniqueCodeRepository(Database.Database db)
        {
            _db = db;
        }

        public async Task SaveUniqueCode(UniqueCode code)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            
            var query = "INSERT INTO link (code, is_used) VALUES ($1, $2)";
            await using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = code.Code });
            sql.Parameters.Add(new NpgsqlParameter { Value = code.IsUsed });

            await sql.ExecuteNonQueryAsync();
        }

        public async Task<long> ResolveLinkIdAsync(Guid uniqueCode)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();

            var query = "SELECT link_id FROM link WHERE code = $1";
            await using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = uniqueCode });

            var result = await sql.ExecuteScalarAsync();
            if (result == null)
            {
                return 0;
            }

            return Convert.ToInt64(result);
        }
    }
}
