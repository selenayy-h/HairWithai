using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hairr.Models;
using System.Collections.Generic;

namespace Hairr.Controllers
{
    public class PersonelimController : Controller
    {
        Context c = new Context();

        [Authorize]
        public IActionResult Index()
        {
            var personeller = c.Personels.Include(x => x.Islem).Include(x => x.CalismaSaatis).ToList();
            return View(personeller);
        }

        [HttpGet] // Sayfa yüklendiğinde çalışır
        public IActionResult YeniPersonel()
        {
            List<SelectListItem> islemler = (from x in c.Islems.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.IslemAdi,
                                                 Value = x.ID.ToString()
                                             }).ToList();

            ViewBag.Islemler = islemler;
            return View();
        }

        [HttpPost] // Form gönderildiğinde çalışır
        public IActionResult YeniPersonel(Personel p, List<CalismaSaati> calismaSaatleri)
        {
            var islem = c.Islems.Where(x => x.ID == p.IslemId).FirstOrDefault();
            p.Islem = islem;

            c.Personels.Add(p);
            c.SaveChanges(); // İlk olarak Personel kaydedilmeli

            foreach (var calismaSaati in calismaSaatleri)
            {
                if (!string.IsNullOrEmpty(calismaSaati.Gun))
                {
                    calismaSaati.PersonelId = p.PersonelId;
                    c.CalismaSaatis.Add(calismaSaati);
                }
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PersonelSil(int id)
        {
            var personel = c.Personels.Include(p => p.CalismaSaatis).FirstOrDefault(p => p.PersonelId == id);
            if (personel != null)
            {
                // İlişkili çalışma saatlerini sil
                c.CalismaSaatis.RemoveRange(personel.CalismaSaatis);

                // Personeli sil
                c.Personels.Remove(personel);
                c.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PersonelGetir(int id)
        {
            var personel = c.Personels.Include(p => p.CalismaSaatis).FirstOrDefault(p => p.PersonelId == id);
            List<SelectListItem> islemler = (from x in c.Islems.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.IslemAdi,
                                                 Value = x.ID.ToString(),
                                                 Selected = x.ID == personel.IslemId
                                             }).ToList();

            ViewBag.Islemler = islemler;
            return View("PersonelGetir", personel);
        }

        [HttpPost]
        public IActionResult PersonelGuncelle(Personel p, List<CalismaSaati> calismaSaatleri)
        {
            var personel = c.Personels.Include(pe => pe.CalismaSaatis).FirstOrDefault(pe => pe.PersonelId == p.PersonelId);
            if (personel != null)
            {
                personel.Ad = p.Ad;
                personel.Soyad = p.Soyad;
                personel.Sehir = p.Sehir;
                personel.IslemId = p.IslemId;

                // Mevcut çalışma saatlerini sil
                c.CalismaSaatis.RemoveRange(personel.CalismaSaatis);

                // Yeni çalışma saatlerini ekle
                foreach (var calismaSaati in calismaSaatleri)
                {
                    if (!string.IsNullOrEmpty(calismaSaati.Gun))
                    {
                        calismaSaati.PersonelId = personel.PersonelId;
                        c.CalismaSaatis.Add(calismaSaati);
                    }
                }

                c.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
