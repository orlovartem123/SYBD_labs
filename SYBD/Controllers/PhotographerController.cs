using Microsoft.AspNetCore.Mvc;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;

namespace SYBD.Controllers
{
    public class PhotographerController : Controller
    {
        private readonly IPhotographerStorage _storage;

        public PhotographerController(IPhotographerStorage storage)
        {
            _storage = storage;
            Program.IsFresh = false;
        }

        public IActionResult PhotographerList()
        {
            var model = _storage.GetFullList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePhotographer()
        {
            return View();
        }

        [HttpPost]
        public void CreatePhotographer(Photographer photographer)
        {
            _storage.Insert(photographer);
            Response.Redirect("PhotographerList");
        }

        [HttpGet]
        public IActionResult UpdatePhotographer(int id)
        {
            var model = _storage.GetElement(id);
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult UpdatePhotographer(Photographer model)
        {
            _storage.Update(model);
            return RedirectToAction("PhotographerList", "Photographer");
        }

        public RedirectToActionResult DeletePhotographer(int id)
        {
            _storage.Delete(id);
            return RedirectToAction("PhotographerList", "Photographer");
        }
    }
}
