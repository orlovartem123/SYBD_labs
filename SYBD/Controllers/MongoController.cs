using Microsoft.AspNetCore.Mvc;
using SYBD.Db.MongoDb;
using System.Threading.Tasks;

namespace SYBD.Controllers
{
    public class MongoController : Controller
    {
        private readonly DbService db;
        public MongoController(DbService context)
        {
            db = context;
        }

        public async Task<IActionResult> StartTransfer()
        {
            await db.StartTransferData();
            Program.IsFresh = true;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Photographers()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var data = await db.GetPhotographers();
            startTime.Stop();
            return View((startTime.Elapsed.ToString(), data));
        }

        public async Task<IActionResult> Genres()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var data = await db.GetGenres();
            startTime.Stop();
            return View((startTime.Elapsed.ToString(), data));
        }

        public async Task<IActionResult> Incomes()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var data = await db.GetIncomes();
            startTime.Stop();
            return View((startTime.Elapsed.ToString(), data));
        }

        public async Task<IActionResult> Sales()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var data = await db.GetSales();
            startTime.Stop();
            return View((startTime.Elapsed.ToString(), data));
        }

        public async Task<IActionResult> Stocks()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var data = await db.GetStocks();
            startTime.Stop();
            return View((startTime.Elapsed.ToString(), data));
        }

        public async Task<IActionResult> Photos()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            var data = await db.GetPhotos();
            startTime.Stop();
            return View((startTime.Elapsed.ToString(), data));
        }
    }
}
