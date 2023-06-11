using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoziMe.Data;
using VoziMe.Models;

namespace VoziMe.Controllers
{
    public class VoznjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoznjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public double Distance(double lat1, double lon1, double lat2, double lon2) {
            var r = 6371;

            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return r * c;
        }

        [HttpPost]
        public async Task<IActionResult> OrderRide(string locationValue, string destinationValue, string xcoord, string ycoord) {
            //if (User.Identity.IsAuthenticated == false) return RedirectToPage("/Account/Login", new { area = "Identity" });
            //if (KlijentController.klijentLokalno == null) return RedirectToAction("Create", "Klijent");

            double x = Double.Parse(xcoord);
            double y = Double.Parse(ycoord);

            var vozaci = _context.Vozac.ToList();
            var najbliziVozac = vozaci.OrderBy(vozac => Distance(x, y, vozac.xkord, vozac.ykord)).FirstOrDefault();
            var cijena = 2.5 + 1.5 * Distance(x, y, najbliziVozac.xkord, najbliziVozac.ykord);
            cijena = Math.Round(cijena, 2);
            var newVoznje = new Voznje {
                vozacId = najbliziVozac.id,
                korisnikId = 6,
                vrijeme = DateTime.Now,
                ocjena = -1,
                cijena = (decimal)cijena,
                adresaPolazista = locationValue,
                adresaDolazista = destinationValue
            };

            _context.Voznje.Add(newVoznje);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderConfirmation));
        }

        public IActionResult OrderConfirmation() {
            var lastOrderedRide = _context.Voznje.OrderByDescending(v => v.id).FirstOrDefault();

            var vozac = _context.Vozac.FirstOrDefault(v => v.id == lastOrderedRide.vozacId);
            var klijent = _context.Klijent.FirstOrDefault(v => v.id == lastOrderedRide.korisnikId);
            var firma = _context.Firma.FirstOrDefault(v => v.id == vozac.firmaId);
            var vozilo = _context.Vozilo.FirstOrDefault(v => v.id == vozac.voziloId);

            ViewBag.Vozac = vozac;
            ViewBag.Klijent = klijent;
            ViewBag.Firma = firma;
            ViewBag.Vozilo = vozilo;

            return View(lastOrderedRide);
        }

        // GET: Voznje
        [Authorize(Roles = "Administrator, Korisnik, Vozac")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Voznje.Include(v => v.Klijent).Include(v => v.Vozac);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Voznje/Details/5
        
        [Authorize(Roles = "Administrator, Korisnik, Vozac")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznje = await _context.Voznje
                .Include(v => v.Klijent)
                .Include(v => v.Vozac)
                .FirstOrDefaultAsync(m => m.id == id);
            if (voznje == null)
            {
                return NotFound();
            }

            return View(voznje);
        }

        // GET: Voznje/Create
        [Authorize(Roles = "Administrator, Korisnik, Vozac")]
        public IActionResult Create()
        {
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "adresa");
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "adresa");
            return View();
        }

        // POST: Voznje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik, Vozac")]
        public async Task<IActionResult> Create([Bind("id,vozacId,korisnikId,vrijeme,ocjena,cijena,adresaPolazista,adresaDolazista")] Voznje voznje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voznje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "adresa", voznje.korisnikId);
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "adresa", voznje.vozacId);
            return View(voznje);
        }

        // GET: Voznje/Edit/5
        
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznje = await _context.Voznje.FindAsync(id);
            if (voznje == null)
            {
                return NotFound();
            }
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "adresa", voznje.korisnikId);
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "adresa", voznje.vozacId);
            return View(voznje);
        }

        // POST: Voznje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik, Vozac")]
        public async Task<IActionResult> Edit(int id, [Bind("id,vozacId,korisnikId,vrijeme,ocjena,cijena,adresaPolazista,adresaDolazista")] Voznje voznje)
        {
            if (id != voznje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voznje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoznjeExists(voznje.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "adresa", voznje.korisnikId);
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "adresa", voznje.vozacId);
            return View(voznje);
        }

        // GET: Voznje/Delete/5
        
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznje = await _context.Voznje
                .Include(v => v.Klijent)
                .Include(v => v.Vozac)
                .FirstOrDefaultAsync(m => m.id == id);
            if (voznje == null)
            {
                return NotFound();
            }

            return View(voznje);
        }

        // POST: Voznje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik, Vozac")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voznje = await _context.Voznje.FindAsync(id);
            _context.Voznje.Remove(voznje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoznjeExists(int id)
        {
            return _context.Voznje.Any(e => e.id == id);
        }
    }
}
