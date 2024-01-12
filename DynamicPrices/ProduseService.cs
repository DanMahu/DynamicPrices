using DynamicPrices.Models;
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

        //functie ce returneaza categoria de produse electronice si numarul de produse disponibile (lista de categorii pentru afisarea acestora in dropdown menu)
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

        //functie ce returneaza detaliile despre produse dupa categorie (pentru afisarea detaliilor intr-un tabel)
        public List<Dictionary<string, object>> GetProduseDupaTip(string tipProdus)
        {
            List<Dictionary<string, object>> produse = new List<Dictionary<string, object>>();
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                //string sql = "select * from produse_electronice where tip_produs = @tipProdus";
                string sql = "select p.nume_produs, p.tip_produs, p.cost_producere, p.pret_recomandat, p.descriere, pe.pret_curent from produse_electronice p join preturi_electronice pe on p.id_produs = pe.id_produs where p.tip_produs = @tipProdus";
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

        //functie ce returneaza id-ul si numele produsului dupa categorie (lista produselor pentru afisarea istoriei de preturi)
        public Dictionary<int, string> GetProduseElectronice(string tipProdus)
        {
            Dictionary<int, string> produse = new Dictionary<int, string>();
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                string sql = "select p.id_produs, p.nume_produs from produse_electronice p where tip_produs = @tipProdus";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tipProdus", tipProdus);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id_produs = reader.GetInt32("id_produs");
                            string nume_produs = reader.GetString("nume_produs");
                            produse.Add(id_produs, nume_produs);
                        }
                    }
                }
            }
            return produse;
        }

        //functie ce returneaza istoria modificarii pretului pentru produsul selectat
        public List<object> GetPriceHistoryByProduct(int product_id)
        {
            List<object> priceHistory = new List<object>();
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                string sql = "select p.nume_produs, i.pret_vechi, i.pret_nou, i.data_modificare from istoric_preturi_electronice i join produse_electronice p on i.id_produs = p.id_produs where p.id_produs = @product_id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@product_id", product_id);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            decimal pret_vechi = reader.GetDecimal(reader.GetOrdinal("pret_vechi"));
                            decimal pret_nou = reader.GetDecimal(reader.GetOrdinal("pret_nou"));
                            DateTime data_mod = reader.GetDateTime(reader.GetOrdinal("data_modificare"));
                            string data_modificare = data_mod.ToString("dd-MM-yyyy");

                            var data = new
                            {
                                pret_vechi,
                                pret_nou,
                                data_modificare
                            };

                            priceHistory.Add(data);
                        }
                    }
                }
            }
            return priceHistory;
        }

        //functie ce returneaza stocul actual al produsului selectat
        public int GetStocActualDupaProdus(int idProdus)
        {
            int cantitate = 0;
            using (MySqlConnection  conn = _databaseService.GetConnection())
            {
                string sql = "select cantitate from stocuri_electronice where id_produs = @idProdus";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("idProdus", idProdus);
                    conn.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cantitate = reader.GetInt32("cantitate");
                        }
                    }
                }
            }
            return cantitate;
        }

        //functie ce returneaza pretul actual al produsului selectat
        public decimal GetPretCurentDupaProdus(int idProdus)
        {
            decimal pret = 0;
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                string sql = "select pret_curent from preturi_electronice where id_produs = @idProdus";
                using (MySqlCommand command = new MySqlCommand (sql, connection))
                {
                    command.Parameters.AddWithValue("idProdus", idProdus);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pret = reader.GetDecimal("pret_curent");
                        }
                    }
                }
            }
            return pret;
        }

        //functie ce returneaza datele despre stocul produsului selectat (min, max, cantitate)
        public List<int> MinMaxStoc(int idProdus)
        {
            List<int> listaStoc = new List<int>();
            using (MySqlConnection conn = _databaseService.GetConnection())
            {
                string sql = "select cantitate, stoc_minim, stoc_maxim from stocuri_electronice where id_produs = @idProdus";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("idProdus", idProdus);
                    conn.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaStoc.Add(reader.GetInt32("cantitate"));
                            listaStoc.Add(reader.GetInt32("stoc_minim"));
                            listaStoc.Add(reader.GetInt32("stoc_maxim"));
                        }
                    }
                }
                return listaStoc;
            }
        }
    }
}
