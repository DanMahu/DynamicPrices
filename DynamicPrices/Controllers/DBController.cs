using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DynamicPrices.Controllers
{
    public class DBController : Controller
    {
        private readonly IConfiguration _configuration;

        public DBController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: DBController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        /*public ActionResult DBTest()
        {
            string connectionString = _configuration.GetConnectionString("MySQLConnection")!;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    ViewBag.Message = "Conexiunea cu MySQL este deschisa!";
                    // alte operatii ca baza de date
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Eroare la conectare: " + ex.Message;
                }
            }
            return View();
        }*/

        public ActionResult DBTest()
        {
            string connectionString = _configuration.GetConnectionString("MySQLConnection")!;
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM clienti";

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
                catch (Exception ex)
                {
                    ViewBag.Message = "Eroare la conectare: " + ex.Message;
                }
            }

            return View(data);
        }
    }
}
