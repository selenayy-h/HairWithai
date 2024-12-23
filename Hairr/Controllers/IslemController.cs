using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hairr.Models;
using System.Linq;

namespace Hairr.Controllers
{

    public class IslemController : Controller
    {
        private readonly Context _context;

        public IslemController()
        {
            _context = new Context();
        }

        // GET: Islem/Index
        public IActionResult Index()
        {
            var islemler = _context.Islems.ToList();
            return View(islemler);
        }

        // GET: Islem/YeniIslem
        [HttpGet]
        public IActionResult YeniIslem()
        {
            return View();
        }

        // POST: Islem/YeniIslem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult YeniIslem(Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islems.Add(islem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(islem);
        }

        // GET: Islem/IslemSil/5
        public IActionResult IslemSil(int id)
        {
            var islem = _context.Islems.Find(id);
            if (islem == null)
            {
                return NotFound();
            }
            return View(islem);
        }

        // POST: Islem/IslemSil/5
        [HttpPost, ActionName("IslemSil")]
        [ValidateAntiForgeryToken]
        public IActionResult IslemSilConfirmed(int id)
        {
            var islem = _context.Islems.Find(id);
            if (islem != null)
            {
                _context.Islems.Remove(islem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Islem/IslemGetir/5
        [HttpGet]
        public IActionResult IslemGetir(int id)
        {
            var islem = _context.Islems.Find(id);
            if (islem == null)
            {
                return NotFound();
            }
            return View(islem);
        }

        // POST: Islem/IslemGuncelle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IslemGuncelle(Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islems.Update(islem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("IslemGetir", islem);
        }
    }
}
