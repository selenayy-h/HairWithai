using Hairr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hairr.Controllers
{
    [Authorize] // Kullanıcının giriş yapmış olması gerekiyor
    public class RnController : Controller
    {
        private readonly Context c = new Context(); // Context nesnesi doğrudan oluşturuluyor

        // GET: Randevu Oluşturma Sayfası
        [HttpGet]
        public IActionResult Create()
        {
            var services = c.Islems.ToList(); // İşlemleri al
            var employees = c.Personels.ToList(); // Personelleri al

            // İşlem ve Personel listesini ViewBag ile View'e gönder
            ViewBag.Services = new SelectList(services, "ID", "IslemAdi");
            ViewBag.Employees = new SelectList(employees, "PersonelId", "Ad");

            return View(); // Randevu oluşturma sayfasını döndür
        }

        // POST: Randevu Kaydetme İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF koruması
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Müşteri adını giriş yapan kullanıcının adı olarak ayarla
                appointment.CustomerName = User.Identity.Name;

                // Randevuyu kaydet
                appointment.Status = "Beklemede"; // Varsayılan durum
                c.Appointments.Add(appointment);
                c.SaveChanges(); // Veritabanına kaydet

                return RedirectToAction("Success"); // Başarı sayfasına yönlendir
            }

            // Model geçersizse işlemleri ve personelleri tekrar yükle
            var servicesList = c.Islems.ToList();
            var employeesList = c.Personels.ToList();
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

            if (string.IsNullOrEmpty(customerName))
            {
                // Eğer kullanıcı oturum açmamışsa, giriş sayfasına yönlendir
                return RedirectToAction("Index", "Login");
            }

            var randevular = c.Appointments
                .Where(a => a.CustomerName == customerName)
                .Include(a => a.Islem) // İşlem bilgisi dahil edilir
                .Include(a => a.Personel) // Personel bilgisi dahil edilir
                .ToList();

            return View(randevular);
        }
    }
}
