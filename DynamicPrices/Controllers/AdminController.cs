using DynamicPrices.Models;
using DynamicPricing.Data;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DynamicPrices.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProduseService _produseService;
        private readonly ApplicationDbContext _db;

        public AdminController(IProduseService produseService, ApplicationDbContext db)
        {
            _produseService = produseService;
            _db = db;
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult TipProduse()
        {
            Dictionary<string, int> tipuriProduse = _produseService.GetTipProduseElectronice();
            var typeArray = tipuriProduse.Select(t => new { TipProdus = t.Key }).ToArray();

            return Json(typeArray);
        }

        public IActionResult AllProduseElectronice()
        {
            //lista de produse electronice
            List<Dictionary<string, object>> produse = _produseService.GetAllProduseElectronice();

            return Json(produse);
        }

        public ActionResult AddProduseElectronice() {

            return View();
        }

        [HttpPost]
        public ActionResult AddProduseElectronice(AddProdusElectronicModel obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Produse_Electronice produsElectronic = new Produse_Electronice
                    {
                        NumeProdus = obj.NumeProdus,
                        TipProdus = obj.TipProdus,
                        CostProducere = obj.CostProducere,
                        PretRecomandat = obj.PretRecomandat,
                        Descriere = obj.Descriere
                    };

                    _db.produse_electronice.Add(produsElectronic);
                    _db.SaveChanges();

                    int idProdus = produsElectronic.IdProdus;
                    Preturi_Electronice pretCurent = new Preturi_Electronice
                    {
                        IdProdus = idProdus,
                        PretCurent = obj.PretCurent,
                        DataActualizare = DateTime.Now
                    };

                    _db.preturi_electronice.Add(pretCurent);
                    _db.SaveChanges();

                    return RedirectToAction("Admin");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View();
            }
        }
    }
}
