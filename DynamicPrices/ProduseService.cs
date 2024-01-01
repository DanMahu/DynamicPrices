using MySql.Data.MySqlClient;

namespace DynamicPrices
{
    public class ProduseService : IProduseService
    {
        private readonly IConfiguration _configuration;

        public ProduseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Dictionary<string, int> GetTipProduseElectronice()
        {
            Dictionary<string, int> tipuriProduse = new Dictionary<string, int>();
            string connectionString = _configuration.GetConnectionString("MySQLConnection")!;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string sql = "select tip_produs, count(*) as numar_produse from produse_electronice group by tip_produs";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipProdus = reader.GetString("tip_produs");
                            int numarProdus = reader.GetInt32("numar_produse");
                            tipuriProduse.Add(tipProdus, numarProdus);
                        }
                    }
                }
            }

            return tipuriProduse;
        }
    }
}
