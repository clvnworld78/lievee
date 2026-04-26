namespace lievee.Database
{
    using Npgsql;
    public class Database
    {
        private static readonly Database _pool = new();
        private const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=test123;Database=lievee;Pooling=true;MinPoolSize=2;MaxPoolSize=10";
        private Database() { }
        public static Database Pool => _pool;
        public NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
