using Hairr.Models;
using Hairr.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hairr.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly Context _context;

        public AppointmentController(Context context)
        {
            _context = context;
        }

        // Randevu Oluşturma Formunu Göster
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateAppointmentViewModel
            {
                Islemler = _context.Islems.Select(i => new SelectListItem
                {
                    Text = i.IslemAdi,
                    Value = i.ID.ToString()
                }).ToList(),
                Personeller = _context.Personels.Select(p => new SelectListItem
                {
                    Text = $"{p.Ad} {p.Soyad}",
                    Value = p.PersonelId.ToString()
                }).ToList()
            };

            return View(viewModel);
        }

        // Randevu Oluşturma İşlemini Gerçekleştir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Hizmet bilgisi al
                var islem = await _context.Islems.FindAsync(model.IslemId);
                if (islem == null)
                {
                    ModelState.AddModelError("", "Seçilen hizmet bulunamadı.");
                    return View(model);
                }

                // Randevunun bitiş zamanını hesapla
                var appointmentEnd = model.AppointmentDate.AddMinutes(islem.Time);

                // Randevunun yapıldığı günün ismini al
                var gunAdi = model.AppointmentDate.DayOfWeek.ToString();

                // Personelin çalışma saatlerini kontrol et
                var calismaSaati = await _context.CalismaSaatis
                    .Where(cs => cs.PersonelId == model.PersonelId && cs.Gun == gunAdi)
                    .FirstOrDefaultAsync();

                if (calismaSaati == null)
                {
                    ModelState.AddModelError("", "Seçilen personel bu günde çalışmıyor.");
                    // Yeniden hizmet ve personel listesini doldur
                    model.Islemler = _context.Islems.Select(i => new SelectListItem
                    {
                        Text = i.IslemAdi,
                        Value = i.ID.ToString()
                    }).ToList();
                    model.Personeller = _context.Personels.Select(p => new SelectListItem
                    {
                        Text = $"{p.Ad} {p.Soyad}",
                        Value = p.PersonelId.ToString()
                    }).ToList();
                    return View(model);
                }

                // Randevunun personelin çalışma saatleri içinde olup olmadığını kontrol et
                var randevuBaslangic = model.AppointmentDate.TimeOfDay;
                var randevuBitis = appointmentEnd.TimeOfDay;

                if (randevuBaslangic < calismaSaati.BaslangicSaati || randevuBitis > calismaSaati.BitisSaati)
                {
                    ModelState.AddModelError("", "Seçilen zaman dilimi personelin çalışma saatleri dışında.");
                    // Yeniden hizmet ve personel listesini doldur
                    model.Islemler = _context.Islems.Select(i => new SelectListItem
                    {
                        Text = i.IslemAdi,
                        Value = i.ID.ToString()
                    }).ToList();
                    model.Personeller = _context.Personels.Select(p => new SelectListItem
                    {
                        Text = $"{p.Ad} {p.Soyad}",
                        Value = p.PersonelId.ToString()
                    }).ToList();
                    return View(model);
                }

                // Personelin seçilen saatte başka bir randevusu olup olmadığını kontrol et
                var isConflict = await _context.Appointments
                    .Where(a => a.PersonelId == model.PersonelId &&
                           a.Status == "Onaylandı" &&
                           ((model.AppointmentDate >= a.AppointmentDate && model.AppointmentDate < a.AppointmentDate.AddMinutes(a.Islem.Time)) ||
                            (appointmentEnd > a.AppointmentDate && appointmentEnd <= a.AppointmentDate.AddMinutes(a.Islem.Time)) ||
                            (model.AppointmentDate <= a.AppointmentDate && appointmentEnd >= a.AppointmentDate.AddMinutes(a.Islem.Time))))
                    .AnyAsync();

                if (isConflict)
                {
                    ModelState.AddModelError("", "Seçilen zaman dilimi için personel uygun değil. Lütfen başka bir zaman seçiniz.");
                    // Yeniden hizmet ve personel listesini doldur
                    model.Islemler = _context.Islems.Select(i => new SelectListItem
                    {
                        Text = i.IslemAdi,
                        Value = i.ID.ToString()
                    }).ToList();
                    model.Personeller = _context.Personels.Select(p => new SelectListItem
                    {
                        Text = $"{p.Ad} {p.Soyad}",
                        Value = p.PersonelId.ToString()
                    }).ToList();
                    return View(model);
                }

                // Yeni randevuyu oluştur
                var appointment = new Appointment
                {
                    IslemId = model.IslemId,
                    PersonelId = model.PersonelId,
                    CustomerName = model.CustomerName,
                    AppointmentDate = model.AppointmentDate,
                    Status = "Beklemede"
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                // Randevu başarıyla oluşturuldu, kullanıcıyı bilgilendir
                TempData["Success"] = "Randevunuz başarıyla oluşturuldu. Onaylandıktan sonra size bildirilecektir.";
                return RedirectToAction("Index", "Home");
            }

            // Model doğrulama başarısızsa, formu yeniden göster
            model.Islemler = _context.Islems.Select(i => new SelectListItem
            {
                Text = i.IslemAdi,
                Value = i.ID.ToString()
            }).ToList();
            model.Personeller = _context.Personels.Select(p => new SelectListItem
            {
                Text = $"{p.Ad} {p.Soyad}",
                Value = p.PersonelId.ToString()
            }).ToList();

            return View(model);
        }

        // Kullanıcıların Randevularını Görüntülemesi
        public async Task<IActionResult> MyAppointments()
        {
            // Kullanıcının ID'sini almanız gerekecek. Örneğin, kullanıcı adını CustomerName olarak kullanabilirsiniz.
            var currentUserName = User.Identity.Name;

            var appointments = await _context.Appointments
                .Include(a => a.Islem)
                .Include(a => a.Personel)
                .Where(a => a.CustomerName == currentUserName)
                .ToListAsync();

            return View(appointments);
        }

        // Randevu İptal Etme
        public async Task<IActionResult> Cancel(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null && appointment.Status == "Beklemede")
            {
                appointment.Status = "İptal Edildi";
                await _context.SaveChangesAsync();
                TempData["Success"] = "Randevunuz başarıyla iptal edildi.";
            }
            else
            {
                TempData["Error"] = "Randevu bulunamadı veya zaten onaylanmış/reddedilmiş olabilir.";
            }

            return RedirectToAction("MyAppointments");
        }
    }
}
