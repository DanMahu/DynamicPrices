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

        public IActionResult Electronice()
        {
            List<ProduseElectroniceCuPretModel> produseCuPretList = (from p in _db.produse_electronice
                                                                  join pe in _db.preturi_electronice
                                                                  on p.IdProdus equals pe.IdProdus
                                                                  select new ProduseElectroniceCuPretModel
                                                                  {
                                                                      IdProdus = p.IdProdus,
                                                                      NumeProdus = p.NumeProdus,
                                                                      TipProdus = p.TipProdus,
                                                                      CostProducere = p.CostProducere,
                                                                      PretRecomandat = p.PretRecomandat,
                                                                      PretCurent = pe.PretCurent,
                                                                      Descriere = p.Descriere

                                                                  }).ToList();

            return View(produseCuPretList);
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

        public IActionResult AddProduseElectronice() {

            return View();
        }

        [HttpPost]
        public IActionResult AddProduseElectronice(AddProdusElectronicModel obj)
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

        public IActionResult ModProduseElectronice()
        {
            List<ProduseElectroniceCuPretModel> produseCuPretList = (from p in _db.produse_electronice
                                                                  join pe in _db.preturi_electronice
                                                                  on p.IdProdus equals pe.IdProdus
                                                                  select new ProduseElectroniceCuPretModel
                                                                  {
                                                                      IdProdus = p.IdProdus,
                                                                      NumeProdus = p.NumeProdus,
                                                                      TipProdus = p.TipProdus,
                                                                      CostProducere = p.CostProducere,
                                                                      PretRecomandat = p.PretRecomandat,
                                                                      PretCurent = pe.PretCurent,
                                                                      Descriere = p.Descriere

                                                                  }).ToList();

            return View(produseCuPretList);
        }

        public IActionResult EditProduseElectronice(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var produseDinDB = (from p in _db.produse_electronice
                                join pe in _db.preturi_electronice
                                on p.IdProdus equals pe.IdProdus
                                where p.IdProdus == id
                                select new ProduseElectroniceCuPretModel
                                {

                                    IdProdus = p.IdProdus,
                                    NumeProdus = p.NumeProdus,
                                    TipProdus = p.TipProdus,
                                    CostProducere = p.CostProducere,
                                    PretRecomandat = p.PretRecomandat,
                                    PretCurent = pe.PretCurent,
                                    Descriere = p.Descriere
                                }).FirstOrDefault();
            if (produseDinDB == null)
            {
                return NotFound();
            }

            return View(produseDinDB);
        }

        [HttpPost]
        public IActionResult EditProduseElectronice(ProduseElectroniceCuPretModel obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //actualizeaza tabelul de produse
                    var produsElectronic = _db.produse_electronice.Find(obj.IdProdus);
                    if (produsElectronic == null)
                    {
                        return NotFound();
                    }

                    produsElectronic.NumeProdus = obj.NumeProdus;
                    produsElectronic.TipProdus = obj.TipProdus;
                    produsElectronic.CostProducere = obj.CostProducere;
                    produsElectronic.PretRecomandat = obj.PretRecomandat;
                    produsElectronic.Descriere = obj.Descriere;

                    _db.produse_electronice.Update(produsElectronic);
                    _db.SaveChanges();

                    //actualizeaza tabelul de preturi
                    var pret = _db.preturi_electronice.Find(obj.IdProdus);
                    if (pret == null)
                    {
                        return NotFound();
                    }

                    decimal pretVechi = pret.PretCurent;

                    pret.PretCurent = obj.PretCurent;
                    pret.DataActualizare = DateTime.Now;

                    _db.preturi_electronice.Update(pret);
                    _db.SaveChanges();

                    //insereaza pretul in istoric
                    if (pretVechi != pret.PretCurent)
                    {
                        var istoricPret = new Istoric_Preturi_Electronice
                        {
                            IdProdus = obj.IdProdus,
                            PretVechi = pretVechi,
                            PretNou = obj.PretCurent,
                            DataModificare = DateTime.Now
                        };

                        _db.istoric_preturi_electronice.Add(istoricPret);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("ModProduseElectronice", "Admin");
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

        public IActionResult DeleteProduseElectronice(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Produse_Electronice? produsDinDB = _db.produse_electronice.Find(id);
            if (produsDinDB == null)
            {
                return NotFound();
            }

            return View(produsDinDB);
        }

        [HttpPost, ActionName("DeleteProduseElectronice")]
        public IActionResult DeleteProduseElectronicePOST(int? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //sterge produsul si datele acestuia din baza de date
                    Produse_Electronice? produsDinDB = _db.produse_electronice.Find(id);
                    if (produsDinDB == null)
                    {
                        return NotFound();
                    }
                    _db.produse_electronice.Remove(produsDinDB);
                    _db.SaveChanges();

                    return RedirectToAction("ModProduseElectronice", "Admin");
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
