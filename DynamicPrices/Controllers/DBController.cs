﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

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
        public IActionResult Produse()
        {
            //lista de produse electronice (categoria)
            Dictionary<string, int> tipuriProduse = _produseService.GetTipProduseElectronice();

            return View(tipuriProduse);
        }

        public IActionResult Servicii()
        {
            return View();
        }

        public IActionResult ProduseElectronice(string tipProdus)
        {
            //lista de produse electronice dupa tip
            List<Dictionary<string, object>> produseDupaTip = _produseService.GetProduseDupaTip(tipProdus);

            return Json(produseDupaTip);
        }

        public IActionResult ListaDeProduse(string tipProdus)
        {
            //lista de produse electronice dupa tip
            Dictionary<int, string> produse = _produseService.GetProduseElectronice(tipProdus);
            var arrayProduse = produse.Select(p => new { id_produs = p.Key, nume_produs = p.Value }).ToArray(); 

            return Json(arrayProduse);
        }

        public IActionResult IstoriePreturiDupaProdus(int product_id)
        {
            //istoria preturilor dupa produs
            List<object> priceHistory = _produseService.GetPriceHistoryByProduct(product_id);

            return Json(priceHistory);
        }

        public IActionResult StocDupaProdus(int idProdus)
        {
            //stocul curent al unui produs
            int stoc = _produseService.GetStocActualDupaProdus(idProdus);

            return Ok(stoc);
        }

        public IActionResult PretDupaProdus(int idProdus)
        {
            //pretul curent al unui produs
            decimal pret = _produseService.GetPretCurentDupaProdus(idProdus);

            return Ok(pret);
        }

        public IActionResult MinMaxStoc(int idProdus)
        {
            List<int> listaStoc = _produseService.MinMaxStoc(idProdus);

            return Json(listaStoc);
        }
    }
}
