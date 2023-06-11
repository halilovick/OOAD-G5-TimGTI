using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        /*public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }*/

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult Onama()
        {
            return View();
        }
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var useri = _context.Klijent.ToList();
            foreach (Klijent user in useri)
            {
                if (user.mailAdresa == email && user.lozinka == password)
                {
                    KlijentController.klijentLokalno = user;
                    var userASP = await _userManager.GetUserAsync(User);
                    Console.WriteLine(userASP.Email);
                    await _userManager.AddToRoleAsync(userASP, "Korisnik");
                    //return Task.FromResult<IActionResult>(RedirectToAction("Index"));
                    await _signInManager.RefreshSignInAsync(userASP);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Onama");
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
