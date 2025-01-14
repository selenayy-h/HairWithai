﻿using Hairr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hairr.Controllers
{
    [AllowAnonymous]
    public class RnController : Controller


    {
        Context c = new Context();// GET: Randevu Oluşturma Sayfası
        [HttpGet]
        public IActionResult Create()
        {
            var services = c.Islems.ToList(); // İşlemleri al
            var employees = c.Personels.ToList(); // Personelleri al

            // İşlem ve Personel listesini ViewBag ile View'e gönder
            ViewBag.Services = services;
            ViewBag.Employees = employees;

            return View(); // Randevu oluşturma sayfasını döndür
        }

        // POST: Randevu Kaydetme İşlemi
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            List<Appointment> existingAppointments = c.Appointments
      .Where(a => a.PersonelId == appointment.PersonelId &&
                  a.AppointmentDate == appointment.AppointmentDate)
      .ToList();


            if (existingAppointments.Any())
            {
                // Çakışma varsa hata mesajı döndür
                ModelState.AddModelError("", "Seçilen tarih ve saat için çalışan uygun değil.");
                return View(appointment);
            }

            // Randevuyu kaydet
            appointment.Status = "Beklemede"; // Varsayılan durum
            c.Appointments.Add(appointment);
            c.SaveChanges(); // Veritabanına kaydet

            return RedirectToAction("Success"); // Başarı sayfasına yönlendir
        }
        // GET: Success Sayfası
        public IActionResult Success()
        {
            return View();
        }

        // Kullanıcının kendi randevularını görmesi için
        public IActionResult MyAppointments()
        {
            // Kullanıcı adı girişten alınır
            var customerName = User.Identity.Name;

            if (string.IsNullOrEmpty(customerName))
            {
                // Eğer kullanıcı oturum açmamışsa, giriş sayfasına yönlendir
                return RedirectToAction("Index", "Login");
            }

            var randevular = c.Appointments
                .Where(a => a.CustomerName == customerName)
                .Include(a => a.Islem) // İşlem bilgisi dahil edilir
                .ToList();

            return View(randevular);
        }


    }
}
