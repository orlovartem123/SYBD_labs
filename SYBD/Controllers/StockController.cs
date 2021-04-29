using Microsoft.AspNetCore.Mvc;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;

namespace SYBD.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockStorage _storage;

        private readonly IRepositoryStorage _repositoryStorage;

        public StockController(IStockStorage storage, IRepositoryStorage repositoryStorage)
        {
            _storage = storage;
            _repositoryStorage = repositoryStorage;
        }

        public IActionResult StockList()
        {
            var model = _storage.GetFullList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateStock()
        {
            return View();
        }

        [HttpPost]
        public void CreateStock(Stock stock)
        {
            _storage.Insert(stock);
            Response.Redirect("StockList");
        }

        [HttpGet]
        public IActionResult UpdateStock(int id)
        {
            var model = _storage.GetElement(id);
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult UpdateStock(Stock model)
        {
            _storage.Update(model);
            return RedirectToAction("StockList", "Stock");
        }

        public RedirectToActionResult DeleteStock(int id)
        {
            _storage.Delete(id);
            return RedirectToAction("StockList", "Stock");
        }

        public IActionResult StocksWorkload()
        {
            var model = _repositoryStorage.GetFullList();
            return View(model);
        }
    }
}
