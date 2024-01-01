using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DynamicPrices.Controllers
{
    public class DBController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProduseService _produseService;

        public DBController(IConfiguration configuration, IProduseService produseService)
        {
            _configuration = configuration;
            _produseService = produseService;
        }

        // GET: DBController
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Produse()
        {
            Dictionary<string, int> tipuriProduse = _produseService.GetTipProduseElectronice();
            ViewBag.tipuriProduseElectronice = tipuriProduse;

            return View();
        }

        public IActionResult DBTest()
        {
            string connectionString = _configuration.GetConnectionString("MySQLConnection")!;
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM produse_electronice";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    ViewBag.Message = "Conexiunea realizata cu succes!";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, object> item = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                item[reader.GetName(i)] = reader.GetValue(i);
                            }

                            data.Add(item);
                        }
                    }
                }
            }

            return View(data);
        }
    }
}
