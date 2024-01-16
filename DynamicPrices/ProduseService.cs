using DynamicPrices.Models;
using MySql.Data.MySqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                string sql = "select TipProdus, count(*) as numar_produse from produse_electronice group by TipProdus";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tipProdus = reader.GetString("TipProdus");
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
                string sql = "select p.NumeProdus, p.TipProdus, p.CostProducere, p.PretRecomandat, p.Descriere, pe.PretCurent from produse_electronice p join preturi_electronice pe on p.IdProdus = pe.IdProdus where p.TipProdus = @tipProdus";
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

        public List<Dictionary<string, object>> GetAllProduseElectronice()
        {
            List<Dictionary<string, object>> produse = new List<Dictionary<string, object>>();
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                string sql = "select p.IdProdus, p.NumeProdus, p.TipProdus, p.CostProducere, p.PretRecomandat, pe.PretCurent, p.Descriere from produse_electronice p join preturi_electronice pe on p.IdProdus = pe.IdProdus";
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    //connection.Open();
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
                string sql = "select p.IdProdus, p.NumeProdus from produse_electronice p where TipProdus = @tipProdus";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@tipProdus", tipProdus);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id_produs = reader.GetInt32("IdProdus");
                            string nume_produs = reader.GetString("NumeProdus");
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
                string sql = "select p.NumeProdus, i.PretVechi, i.PretNou, i.DataModificare from istoric_preturi_electronice i join produse_electronice p on i.IdProdus = p.IdProdus where p.IdProdus = @product_id";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@product_id", product_id);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            decimal pret_vechi = reader.GetDecimal(reader.GetOrdinal("PretVechi"));
                            decimal pret_nou = reader.GetDecimal(reader.GetOrdinal("PretNou"));
                            DateTime data_mod = reader.GetDateTime(reader.GetOrdinal("DataModificare"));
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
            using (MySqlConnection conn = _databaseService.GetConnection())
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
                string sql = "select PretCurent from preturi_electronice where IdProdus = @idProdus";
                using (MySqlCommand command = new MySqlCommand (sql, connection))
                {
                    command.Parameters.AddWithValue("idProdus", idProdus);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pret = reader.GetDecimal("PretCurent");
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
