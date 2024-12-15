///using Hairr.Migrations;
using Hairr.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Hairr.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context c = new Context();

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(User user)
        {
            // Admin tablosunu kontrol et
            var adminData = c.Admins.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (adminData != null)
            {
                // Admin kullanıcı için Claims oluştur
                var adminClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, adminData.UserName),
            new Claim(ClaimTypes.Role, "A") // Admin rolü
        };

                var adminIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var adminPrincipal = new ClaimsPrincipal(adminIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminPrincipal);
                return RedirectToAction("Index", "Personelim"); // Admin sayfasına yönlendir
            }

            // User tablosunu kontrol et
            var userData = c.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (userData != null)
            {
                // Normal kullanıcı için Claims oluştur
                var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userData.UserName),
            new Claim(ClaimTypes.Role, "User") // User rolü
        };

                var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                return RedirectToAction("Index", "Man"); // Kullanıcı sayfasına yönlendir
            }

            // Kullanıcı veya admin bulunamazsa hata mesajı göster
            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            // Kullanıcının çıkış yapması için mevcut oturumu sonlandırıyoruz
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Çıkış yaptıktan sonra giriş sayfasına yönlendiriyoruz
            return RedirectToAction("Index", "Login");
        }
    }
}