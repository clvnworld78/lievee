using lievee.Database;
using lievee.Models;
using Npgsql;

namespace lievee.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly Database.Database _db;
        public RegistrationRepository(Database.Database db)
        {
            _db = db;
        }

        public List<RegisteredVisitor> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate)
        {
            var data = new List<RegisteredVisitor>();
            using var dbConn = _db.GetConnection();
            // using var tx = dbConn.BeginTransaction();

            using var query = new NpgsqlCommand("SELECT visitor_id, name, phone_number, visit_date FROM visitors", dbConn);
            using var reader = query.ExecuteReader();

            while (reader.Read())
            {
                data.Add(new RegisteredVisitor
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    PhoneNumber = reader.GetInt32(2),
                    Date = reader.GetDateTime(3).ToString("dd-MM-yyyy"),
                });
            }

            return data;
        }

        public async Task SaveNewVisitor(Visitor visitor)
        {
            using var dbConn = _db.GetConnection();

            var query = """
                INSERT INTO visitors (link_id, name, phone_number, visit_date)
                VALUES ($1, $2, $3, $4)
            """;
            using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = visitor.LinkCode });
            sql.Parameters.Add(new NpgsqlParameter { Value = visitor.Name });
            sql.Parameters.Add(new NpgsqlParameter { Value = visitor.PhoneNumber });
            sql.Parameters.Add(new NpgsqlParameter { Value = visitor.Date });

            await sql.ExecuteNonQueryAsync();
        }
    }
}
