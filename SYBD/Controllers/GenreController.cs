using Microsoft.AspNetCore.Mvc;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;

namespace SYBD.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreStorage _storage;
        public GenreController(IGenreStorage storage)
        {
            _storage = storage;
        }

        public IActionResult GenreList()
        {
            var model = _storage.GetFullList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateGenre()
        {
            return View();
        }

        [HttpPost]
        public void CreateGenre(Genre genre)
        {
            _storage.Insert(genre);
            Response.Redirect("GenreList");
        }

        [HttpGet]
        public IActionResult UpdateGenre(int id)
        {
            var model = _storage.GetElement(id);
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult UpdateGenre(Genre model)
        {
            _storage.Update(model);
            return RedirectToAction("GenreList", "Genre");
        }

        public RedirectToActionResult DeleteGenre(int id)
        {
            _storage.Delete(id);
            return RedirectToAction("GenreList", "Genre");
        }
    }
}
