﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hairr.Models;

namespace Hairr.Controllers
{
    [Authorize(Roles = "A")] // Tüm Controller için sadece "A" rolü erişebilir
    public class PersonelimController : Controller
    {
        Context c = new Context();

        // Personel Listesi: Tüm personelleri görüntüler
        public IActionResult Index()
        {
            var degerler = c.Personels.Include(x => x.Islem).ToList();
            return View(degerler);
        }

        // Yeni Personel Ekleme Sayfası (GET)
        // GET: YeniPersonel
        [HttpGet]
        public IActionResult YeniPersonel()
        {
            // DropdownList için veriyi doldur
            ViewBag.islemUzmanliklari = c.Islems
                .Select(x => new SelectListItem
                {
                    Text = x.IslemAdi,
                    Value = x.ID.ToString()
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult YeniPersonel(Personel p)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ErrorMessage"] = "Hata: " + string.Join(", ", errors);

                // İşlem uzmanlığı listesini tekrar doldur
                ViewBag.islemUzmanliklari = c.Islems
                    .Select(x => new SelectListItem { Text = x.IslemAdi, Value = x.ID.ToString() }).ToList();

                return View(p);
            }

            // İşlem uzmanlığı verisini veritabanından çek
            var islem = c.Islems.FirstOrDefault(x => x.ID == p.IslemId);
            if (islem != null)
            {
                p.Islem = islem;
            }

            // Yeni personel ekle
            c.Personels.Add(p);
            c.SaveChanges();

            TempData["SuccessMessage"] = "Personel başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public IActionResult PersonelSil(int id)
        {
            var personel = c.Personels.Find(id);

            if (personel != null)
            {
                c.Personels.Remove(personel);
                c.SaveChanges(); // İlişkili tüm veriler otomatik olarak silinecek
            }

            return RedirectToAction("Index");
        }


        // Personel Güncelleme Sayfası (GET)
        public IActionResult PersonelGetir(int id)
        {
            var personel = c.Personels.Find(id);
            if (personel == null)
            {
                return NotFound();
            }

            List<SelectListItem> degerler = (from x in c.Islems.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.IslemAdi,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("PersonelGetir", personel);
        }

        // Personel Güncelleme İşlemi (POST)
        [HttpPost]
        public IActionResult PersonelGuncelle(Personel p)
        {
            var personel = c.Personels.Find(p.PersonelId);
            if (personel != null)
            {
                personel.Ad = p.Ad;
                personel.Soyad = p.Soyad;
                personel.Sehir = p.Sehir;
                personel.IslemId = p.IslemId;

                c.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
