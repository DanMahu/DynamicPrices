using MySql.Data.MySqlClient;

namespace DynamicPrices
{
    public class ProduseService : IProduseService
    {
        private readonly IConfiguration _configuration;
        private readonly DatabaseService _databaseService;

        public ProduseService(IConfiguration configuration, DatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }

        public Dictionary<string, int> GetTipProduseElectronice()
        {
            Dictionary<string, int> tipuriProduse = new Dictionary<string, int>();
            using (MySqlConnection connection = _databaseService.GetConnection())
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

        public List<Dictionary<string, object>> GetProduseDupaTip(string tipProdus)
        {
            List<Dictionary<string, object>> produse = new List<Dictionary<string, object>>();
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                string sql = "select * from produse_electronice where tip_produs = @tipProdus";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tipProdus", tipProdus);
                    connection.Open();
                    using (MySqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Dictionary<string, object> produs = new Dictionary<string, object>();
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                produs[rdr.GetName(i)] = rdr.GetValue(i);
                            }
                            produse.Add(produs);
                        }
                    }
                }
            }
            
            return produse;
        }

    }
}
