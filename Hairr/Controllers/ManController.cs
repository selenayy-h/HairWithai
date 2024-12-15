using Hairr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hairr.Controllers
{
    [Authorize(Roles = "User")]
    public class ManController : Controller
    {

        Context c = new Context();
        // Yalnızca adminler erişebilir
        public IActionResult Index()
        {
            var degerler = c.Islems.ToList();

            return View(degerler);
        }


        public IActionResult PersonelDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.IslemId == id).ToList();


            var brmad = c.Islems.Where(x => x.ID == id).Select(y => y.IslemAdi).FirstOrDefault();

            ViewBag.brm = brmad;
            return View(degerler);
        }




        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            // Veritabanına kaydet
            c.Appointments.Add(appointment);
            c.SaveChanges();

            return Ok(); // Başarılı işlem mesajı döner
        }








    }
}