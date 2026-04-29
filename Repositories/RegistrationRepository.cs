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

            var query = "SELECT visitor_id, link_id, name, phone_number, visit_date FROM visitors";
            using var sql = new NpgsqlCommand(query, dbConn);
            using var reader = sql.ExecuteReader();

            while (reader.Read())
            {
                var today = DateTime.Now;
                var dateOnly = DateOnly.FromDateTime(today);

                var registVisitor = RegisteredVisitor.NewRegisteredVisitor
                (
                    reader.GetInt32(0),
                    reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), 
                    DateOnly.FromDateTime(reader.GetDateTime(3))
                );

                data.Add(registVisitor);
            }

            return data;
        }

        public async Task<RegisteredVisitor?> ResolveVisitorId(int visitorId)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();

            var query = "SELECT (link_id, name, phone_number, visit_date) FROM visitors WHERE visitor_id = $1";
            using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = visitorId });

            using var reader = await sql.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return RegisteredVisitor.NewRegisteredVisitor(
                    visitorId,
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    DateOnly.FromDateTime(reader.GetDateTime(3))
                );
            }

            return null;
        }

        public async Task SaveNewVisitor(RegisteredVisitor visitor)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            await using var tx = await dbConn.BeginTransactionAsync();

            try
            {
                var query = """
                    INSERT INTO visitors (link_id, name, phone_number, visit_date)
                    VALUES ($1, $2, $3, $4)
                """;
                using var sql = new NpgsqlCommand(query, dbConn);
                sql.Parameters.Add(new NpgsqlParameter { Value = visitor.LinkId });
                sql.Parameters.Add(new NpgsqlParameter { Value = visitor.Name });
                sql.Parameters.Add(new NpgsqlParameter { Value = visitor.PhoneNumber });
                sql.Parameters.Add(new NpgsqlParameter { Value = visitor.Date });

                var updateQuery = "UPDATE link SET (is_used = false) WHERE link_id = $1";

                using var updateSql = new NpgsqlCommand(updateQuery, dbConn);
                updateSql.Parameters.Add(new NpgsqlParameter { Value = visitor.LinkId });

                await sql.ExecuteNonQueryAsync();
                await updateSql.ExecuteNonQueryAsync();

                await tx.CommitAsync();
            } catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteRegisteredVisitor(RegisteredVisitor visitor)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            using var tx = await dbConn.BeginTransactionAsync();

            try
            {
                var delVisitorQuery = "DELETE FROM visitors WHERE visitor_id = $1";
                var delLinkQuery = "DELETE FROM link WHERE link_id = $1";

                using var visitorSql = new NpgsqlCommand(delVisitorQuery, dbConn);
                visitorSql.Parameters.Add(new NpgsqlParameter { Value = visitor.VisitorId });

                using var linkSql = new NpgsqlCommand(delLinkQuery, dbConn);
                linkSql.Parameters.Add(new NpgsqlParameter { Value = visitor.LinkId });

                await visitorSql.ExecuteReaderAsync();
                await linkSql.ExecuteReaderAsync();

                await tx.CommitAsync();
            } catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
