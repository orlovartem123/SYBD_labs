using Microsoft.AspNetCore.Mvc;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;

namespace SYBD.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleStorage _saleStorage;

        private readonly IStockStorage _stockStorage;

        private readonly IPhotoStorage _photoStorage;

        public SaleController(ISaleStorage saleStorage, IStockStorage stockStorage, IPhotoStorage photoStorage)
        {
            _saleStorage = saleStorage;
            _stockStorage = stockStorage;
            _photoStorage = photoStorage;
        }

        public IActionResult SaleList()
        {
            var model = _saleStorage.GetFullList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSale()
        {
            ViewBag.Photos = _photoStorage.GetFullList();
            ViewBag.Stocks = _stockStorage.GetFullList();
            return View();
        }

        [HttpPost]
        public RedirectToActionResult CreateSale(Sale model)
        {
            _saleStorage.Insert(model);
            return RedirectToAction("SaleList", "Sale");
        }

        public RedirectToActionResult DeleteSale(int id)
        {
            _saleStorage.Delete(id);
            return RedirectToAction("SaleList", "Sale");
        }
    }
}
