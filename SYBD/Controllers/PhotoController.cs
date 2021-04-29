using Microsoft.AspNetCore.Mvc;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;

namespace SYBD.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoStorage _photoStorage;

        private readonly IGenreStorage _genreStorage;

        private readonly IPhotographerStorage _photographerStorage;

        public PhotoController(IPhotoStorage photoStorage, IGenreStorage genreStorage,
            IPhotographerStorage photographerStorage)
        {
            _photoStorage = photoStorage;
            _genreStorage = genreStorage;
            _photographerStorage = photographerStorage;
        }

        public IActionResult PhotoList()
        {
            var model = _photoStorage.GetFullList();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreatePhoto()
        {
            ViewBag.Genres = _genreStorage.GetFullList();
            ViewBag.Photographers = _photographerStorage.GetFullList();
            return View();
        }

        [HttpPost]
        public void CreatePhoto(Photo photo)
        {
            _photoStorage.Insert(photo);
            Response.Redirect("PhotoList");
        }

        [HttpGet]
        public IActionResult UpdatePhoto(int id)
        {
            var model = _photoStorage.GetElement(id);
            ViewBag.Genres = _genreStorage.GetFullList();
            ViewBag.Photographers = _photographerStorage.GetFullList();
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult UpdatePhoto(Photo model)
        {
            _photoStorage.Update(model);
            return RedirectToAction("PhotoList", "Photo");
        }

        public RedirectToActionResult DeletePhoto(int id)
        {
            _photoStorage.Delete(id);
            return RedirectToAction("PhotoList", "Photo");
        }
    }
}
