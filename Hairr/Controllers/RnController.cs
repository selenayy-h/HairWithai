using Hairr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hairr.Controllers
{
    [Authorize] // Kullanıcı giriş yapmış olmalı
    public class RnController : Controller
    {
        private readonly Context _context;

        public RnController(Context context)
        {
            _context = context;
        }

        // GET: Randevu Oluşturma Sayfası
        [HttpGet]
        public IActionResult Create()
        {
            var services = _context.Islems.ToList(); // İşlemleri al
            var employees = _context.Personels.ToList(); // Personelleri al

            // İşlem ve Personel listesini ViewBag ile View'e gönder
            ViewBag.Services = new SelectList(services, "ID", "IslemAdi");
            ViewBag.Employees = new SelectList(employees, "PersonelId", "Ad");

            return View(); // Randevu oluşturma sayfasını döndür
        }

        // POST: Randevu Kaydetme İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Müşteri adını giriş yapan kullanıcının adı olarak ayarla
                appointment.CustomerName = User.Identity.Name;

                // Çakışma kontrolü
                bool conflict = _context.Appointments
                    .Any(a => a.PersonelId == appointment.PersonelId &&
                              a.AppointmentDate == appointment.AppointmentDate);

                if (conflict)
                {
                    ModelState.AddModelError("", "Seçilen tarih ve saat için çalışan uygun değil.");
                    // İşlem ve Personel listesini tekrar yükle
                    var services = _context.Islems.ToList();
                    var employees = _context.Personels.ToList();
                    ViewBag.Services = new SelectList(services, "ID", "IslemAdi");
                    ViewBag.Employees = new SelectList(employees, "PersonelId", "Ad");
                    return View(appointment);
                }

                // Randevuyu kaydet
                appointment.Status = "Beklemede"; // Varsayılan durum
                _context.Appointments.Add(appointment);
                _context.SaveChanges(); // Veritabanına kaydet

                return RedirectToAction("Success"); // Başarı sayfasına yönlendir
            }

            // Model geçersizse işlemleri ve personelleri tekrar yükle
            var servicesList = _context.Islems.ToList();
            var employeesList = _context.Personels.ToList();
            ViewBag.Services = new SelectList(servicesList, "ID", "IslemAdi");
            ViewBag.Employees = new SelectList(employeesList, "PersonelId", "Ad");

            return View(appointment);
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

            var randevular = _context.Appointments
                .Where(a => a.CustomerName == customerName)
                .Include(a => a.Islem) // İşlem bilgisi dahil edilir
                .Include(a => a.Personel) // Personel bilgisi dahil edilir
                .ToList();

            return View(randevular);
        }
    }
}
