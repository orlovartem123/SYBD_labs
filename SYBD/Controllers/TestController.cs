using Microsoft.AspNetCore.Mvc;
using SYBD.Storages.Interfaces;

namespace SYBD.Controllers
{
    public class TestController : Controller
    {
        private readonly IPhotographerStorage _photographerStorage;

        public TestController(IPhotographerStorage photographerStorage)
        {
            _photographerStorage = photographerStorage;
        }

        public IActionResult Photographer()
        {
            var model = _photographerStorage.PhotographerTest();
            return View(model);
        }

        public IActionResult PhotographerPhoto()
        {
            var model = _photographerStorage.PhotographerPhotoTest();
            return View(model);
        }

        public IActionResult PhotographerPhotoPeriod()
        {
            var model = _photographerStorage.PhotographerPhotoPeriodTest();
            return View(model);
        }
    }
}
