using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VoziMe.Data;
using VoziMe.Models;

namespace VoziMe.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() {
            return View();
        }

        [Authorize(Roles = "Administrator, Korisnik")]
        public IActionResult Privacy() {
            return View();
        }

        [Authorize(Roles = "Administrator, Korisnik")]
        public IActionResult Onama()
        {
            return View();
        }
        [Authorize(Roles = "Administrator, Korisnik")]
        public IActionResult Podrska()
        {
            return View();
        }
        public IActionResult Start()
        {
            return View();
        }
        public IActionResult Login()
        {
            if(KlijentController.klijentLokalno != null) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var useri = _context.Klijent.ToList();
            var vozaci = _context.Vozac.ToList();
            foreach (Klijent user in useri)
            {
                if (user.mailAdresa == email && user.lozinka == password)
                {
                    KlijentController.klijentLokalno = user;
                    var userASP = await _userManager.GetUserAsync(User);
                    await _userManager.AddToRoleAsync(userASP, "Korisnik");
                    await _signInManager.RefreshSignInAsync(userASP);
                    return RedirectToAction("Index");
                }
            }
            foreach (Vozac vozac in vozaci)
            {
                if (vozac.mailAdresa == email && vozac.lozinka == password)
                {
                    VozacController.vozacLokalno = vozac;
                    var userASP = await _userManager.GetUserAsync(User);
                    await _userManager.AddToRoleAsync(userASP, "Vozac");
                    await _signInManager.RefreshSignInAsync(userASP);
                    return RedirectToAction("Index");
                }
            }
            return Error();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
