using MySql.Data.MySqlClient;

namespace DynamicPrices
{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnection GetConnection()
        {
            string connectionString = _configuration.GetConnectionString("MySQLConnection")!;
            return new MySqlConnection(connectionString);
        }
    }
}
