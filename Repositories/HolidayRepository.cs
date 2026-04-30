using lievee.Models;
using Npgsql;

namespace lievee.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly Database.Database _db;
        public HolidayRepository(Database.Database db) { _db = db; }

        public async Task SaveNewHoliday(Holiday newHoliday)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            using var tx = await dbConn.BeginTransactionAsync();

            try
            {
                var query = "INSERT INTO holidays (user_id, holiday) VALUES ($1, $2)";
                using var sql = new NpgsqlCommand(query, dbConn);
                sql.Parameters.Add(new NpgsqlParameter { Value = newHoliday.UserId });
                sql.Parameters.Add(new NpgsqlParameter { Value = newHoliday.Date });
                await sql.ExecuteNonQueryAsync();

                await tx.CommitAsync();
            } catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Holiday>> GetHoliday(DateOnly startDate, DateOnly endDate)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();

            var query = """
                SELECT h.holiday_id, h.user_id, u.username, h.holiday
                FROM holidays h
                JOIN users u ON h.user_id = u.user_id
                WHERE h.holiday BETWEEN $1 AND $2
            """;
            using var sql = new NpgsqlCommand(query, dbConn);
            sql.Parameters.Add(new NpgsqlParameter { Value = startDate });
            sql.Parameters.Add(new NpgsqlParameter { Value = endDate });
            using var reader = await sql.ExecuteReaderAsync();

            var holidays = new List<Holiday>();
            while (reader.Read())
            {
                var holiday = Holiday.NewRegisteredHoliday(
                    reader.GetInt64(0),
                    reader.GetInt64(1),
                    reader.GetString(2),
                    DateOnly.FromDateTime(reader.GetDateTime(3))
                );

                holidays.Add(holiday);
            }

            if (holidays.Count == 0)
            {
                return [];
            }

            return holidays;
        }

        public async Task UpdateHoliday(long holidayId, DateOnly newDate)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            using var tx = await dbConn.BeginTransactionAsync();

            try
            {
                var query = "UPDATE holidays SET holiday = $1 WHERE holiday_id = $2";
                using var sql = new NpgsqlCommand(query, dbConn);
                sql.Parameters.Add(new NpgsqlParameter { Value = newDate });
                sql.Parameters.Add(new NpgsqlParameter { Value = holidayId });

                await sql.ExecuteNonQueryAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteHoliday(long holidayId)
        {
            using var dbConn = _db.GetConnection();
            await dbConn.OpenAsync();
            using var tx = await dbConn.BeginTransactionAsync();

            try
            {
                var query = "DELETE FROM holidays WHERE holiday_id = $1";
                using var sql = new NpgsqlCommand(query, dbConn);
                sql.Parameters.Add(new NpgsqlParameter { Value = holidayId });
                await sql.ExecuteNonQueryAsync();

                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
