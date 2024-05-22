using AracParca.Data;
using AracParca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AracParca.Controllers
{
    public class HesapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HesapController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegister(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.users.FirstOrDefault(u => u.EMail == user.EMail);
                if (existingUser != null)
                {
                    TempData["RegisterMsg"] = "Bu e-posta adresi zaten kullanılıyor.";
                    return View("Register");
                }

                _context.users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                TempData["RegisterMsg"] = "Lütfen Bilgileri Eksiksiz ve Doğru Girin!";
            }
            return View("Register");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(User user)
        {
            var loginUser = _context.users.FirstOrDefault(u => u.EMail == user.EMail && u.Password == user.Password);
            if (loginUser != null)
            {
                HttpContext.Session.SetInt32("UserID", loginUser.Id);
                HttpContext.Session.SetString("User", loginUser.UserName);
                return RedirectToAction("Index", "Parca");
            }
            else
            {
                TempData["LoginMsg"] = "Geçersiz e-posta adresi veya şifre.";
                return View("Login");
            }
        }
    }

}
