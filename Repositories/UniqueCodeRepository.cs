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
            
            var query = "INSERT INTO link (code, is_used) VALUES (@code, @is_used)";
            await using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.AddWithValue(code.Code);
            sql.Parameters.AddWithValue(code.IsUsed);

            await sql.ExecuteNonQueryAsync();
        }
    }
}
