using Microsoft.AspNetCore.Mvc;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;

namespace SYBD.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IIncomeStorage _incomeStorage;

        private readonly IStockStorage _stockStorage;

        private readonly IPhotoStorage _photoStorage;

        public IncomeController(IIncomeStorage incomeStorage, IStockStorage stockStorage, IPhotoStorage photoStorage)
        {
            _incomeStorage = incomeStorage;
            _stockStorage = stockStorage;
            _photoStorage = photoStorage;
        }

        public IActionResult IncomeList()
        {
            var model = _incomeStorage.GetFullList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateIncome()
        {
            ViewBag.Photos = _photoStorage.GetFullList();
            ViewBag.Stocks = _stockStorage.GetFullList();
            return View();
        }

        [HttpPost]
        public RedirectToActionResult CreateIncome(Income model)
        {
            _incomeStorage.Insert(model);
            return RedirectToAction("IncomeList", "Income");
        }

        public RedirectToActionResult DeleteIncome(int id)
        {
            _incomeStorage.Delete(id);
            return RedirectToAction("IncomeList", "Income");
        }
    }
}
