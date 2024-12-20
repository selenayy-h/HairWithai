using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hairr.Models;

namespace Hairr.Controllers
{
    [Authorize(Roles = "A")] // Sadece "A" rolü erişebilir
    public class PersonelimController : Controller
    {
        private readonly Context _context = new Context();

        // Personel Listesi: Tüm personelleri görüntüler
        public IActionResult Index()
        {
            var personeller = _context.Personels
                .Include(p => p.CalismaSaatis) // Çalışma saatlerini dahil et
                .Include(p => p.Islem) // İşlem uzmanlığını dahil et
                .ToList();

            return View(personeller);
        }

        // Yeni Personel Ekleme Sayfası (GET)
        [HttpGet]
        public IActionResult YeniPersonel()
        {
            // İşlem Uzmanlıkları için DropdownList verisini doldur
            ViewBag.islemUzmanliklari = _context.Islems
                .Select(x => new SelectListItem
                {
                    Text = x.IslemAdi,
                    Value = x.ID.ToString()
                }).ToList();

            return View(new Personel());
        }

        [HttpPost]
        public IActionResult YeniPersonel(Personel personel)
        {
            // Gelen verileri kontrol etmek için
            Console.WriteLine($"Ad: {personel.Ad}, Soyad: {personel.Soyad}, Sehir: {personel.Sehir}, IslemId: {personel.IslemId}");

            if (personel.CalismaSaatis != null)
            {
                foreach (var calismaSaati in personel.CalismaSaatis)
                {
                    Console.WriteLine($"Gun: {calismaSaati.Gun}, Baslangic: {calismaSaati.BaslangicSaati}, Bitis: {calismaSaati.BitisSaati}");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Personels.Add(personel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Hata varsa dönen mesajları yazdırın
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Hata: {error.ErrorMessage}");
            }

            return View(personel);
        }

        // Personel Silme
        public IActionResult PersonelSil(int id)
        {
            var personel = _context.Personels
                .Include(p => p.CalismaSaatis)
                .FirstOrDefault(p => p.PersonelId == id);

            if (personel != null)
            {
                _context.Personels.Remove(personel);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Personel Güncelleme Sayfası (GET)
        [HttpGet]
        public IActionResult PersonelGetir(int id)
        {
            var personel = _context.Personels
                .Include(p => p.CalismaSaatis)
                .FirstOrDefault(p => p.PersonelId == id);

            if (personel == null)
            {
                return NotFound();
            }

            // Dropdown verisi
            ViewBag.islemUzmanliklari = _context.Islems
                .Select(x => new SelectListItem
                {
                    Text = x.IslemAdi,
                    Value = x.ID.ToString()
                }).ToList();

            return View(personel);
        }

        // Personel Güncelleme İşlemi (POST)
        [HttpPost]
        public IActionResult PersonelGuncelle(Personel personel)
        {
            var existingPersonel = _context.Personels
                .Include(p => p.CalismaSaatis)
                .FirstOrDefault(p => p.PersonelId == personel.PersonelId);

            if (existingPersonel != null)
            {
                existingPersonel.Ad = personel.Ad;
                existingPersonel.Soyad = personel.Soyad;
                existingPersonel.Sehir = personel.Sehir;
                existingPersonel.IslemId = personel.IslemId;

                // Çalışma saatlerini güncelle
                existingPersonel.CalismaSaatis.Clear();
                if (personel.CalismaSaatis != null && personel.CalismaSaatis.Any())
                {
                    foreach (var calismaSaati in personel.CalismaSaatis)
                    {
                        existingPersonel.CalismaSaatis.Add(calismaSaati);
                    }
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Çalışma Saatlerini Listele
        public IActionResult CalismaSaatleri(int personelId)
        {
            var personel = _context.Personels
                .Include(p => p.CalismaSaatis)
                .FirstOrDefault(p => p.PersonelId == personelId);

            if (personel == null)
                return NotFound();

            return View(personel);
        }

        // Çalışma Saatleri Ekleme Sayfası (GET)
        [HttpGet]
        public IActionResult YeniCalismaSaati(int personelId)
        {
            ViewBag.PersonelId = personelId;
            return View(new CalismaSaati());
        }

        // Çalışma Saatleri Ekleme (POST)
        [HttpPost]
        public IActionResult YeniCalismaSaati(CalismaSaati calismaSaati)
        {
            if (ModelState.IsValid)
            {
                _context.CalismaSaatis.Add(calismaSaati);
                _context.SaveChanges();
                return RedirectToAction("CalismaSaatleri", new { personelId = calismaSaati.PersonelId });
            }

            return View(calismaSaati);
        }
    }
}
