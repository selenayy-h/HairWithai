using Hairr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hairr.Controllers
{
    public class AdminController : Controller
    {
        // Context nesnesi doğrudan burada oluşturuluyor
        Context c = new Context();

        // Randevuları Onaylama Sayfası
        public IActionResult ApproveAppointments()
        {
            var randevular = c.Appointments
                .Where(a => a.Status == "Beklemede")
                .ToList();

            return View(randevular);
        }

        // Randevuyu Onayla
        public IActionResult Onayla(int id)
        {
            var randevu = c.Appointments.FirstOrDefault(a => a.ID == id);
            if (randevu != null)
            {
                randevu.Status = "Onaylandı";
                c.SaveChanges();
            }

            return RedirectToAction("ApproveAppointments");
        }

        // Randevuyu Reddet
        public IActionResult Reddet(int id)
        {
            var randevu = c.Appointments.FirstOrDefault(a => a.ID == id);
            if (randevu != null)
            {
                randevu.Status = "Reddedildi";
                c.SaveChanges();
            }

            return RedirectToAction("ApproveAppointments");
        }
    }
}
