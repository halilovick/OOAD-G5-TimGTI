using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoziMe.Data;
using VoziMe.Models;

namespace VoziMe.Controllers {
    public class VoznjeController : Controller {
        private readonly ApplicationDbContext _context;

        public VoznjeController(ApplicationDbContext context) {
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
            if (User.Identity.IsAuthenticated == false) return RedirectToPage("/Account/Login", new { area = "Identity" });
            if (KlijentController.klijentLokalno == null) return RedirectToAction("Create", "Klijent");

            double x = Double.Parse(xcoord);
            double y = Double.Parse(ycoord);

            var vozaci = _context.Vozac.ToList();
            var najbliziVozac = vozaci.OrderBy(vozac => Distance(x, y, vozac.xkord, vozac.ykord)).FirstOrDefault();
            var cijena = 2.5 + 1.5 * Distance(x, y, najbliziVozac.xkord, najbliziVozac.ykord);
            cijena = Math.Round(cijena, 2);
            var newVoznje = new Voznje {
                vozacId = najbliziVozac.id,
                korisnikId = KlijentController.klijentLokalno.id,
                firmaId = 1,
                voziloId = 1,
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
            var firma = _context.Firma.FirstOrDefault(v => v.id == lastOrderedRide.firmaId);
            var vozilo = _context.Vozilo.FirstOrDefault(v => v.id == lastOrderedRide.voziloId);

            ViewBag.Vozac = vozac;
            ViewBag.Klijent = klijent;
            ViewBag.Firma = firma;
            ViewBag.Vozilo = vozilo;

            return View(lastOrderedRide);
        }

        // GET: Voznje
        [Authorize(Roles = "Administrator, Korisnik, Vozac, Firma, Klijent")]
        public async Task<IActionResult> Index() {
            //var applicationDbContext1 = _context.Voznje.Where(v => v.korisnikId == KlijentController.klijentLokalno.id).Include(v => v.Firma).Include(v => v.Klijent).Include(v => v.Vozac).Include(v => v.Vozilo);
            //var applicationDbContext2 = _context.Voznje.Include(v => v.Firma).Include(v => v.Klijent).Include(v => v.Vozac).Include(v => v.Vozilo);

            if (KlijentController.klijentLokalno != null) {
                return View(await _context.Voznje.Where(v => v.korisnikId == KlijentController.klijentLokalno.id).Include(v => v.Firma).Include(v => v.Vozilo).Include(v => v.Klijent).Include(v => v.Vozac).ToListAsync());
            } else if (User.IsInRole("Administrator")) {
                return View(await _context.Voznje.Include(v => v.Firma).Include(v => v.Vozilo).Include(v => v.Klijent).Include(v => v.Vozac).ToListAsync());
            }
            return RedirectToAction("Create", "Klijent");
        }

        // GET: Voznje/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var voznje = await _context.Voznje
                .Include(v => v.Firma)
                .Include(v => v.Klijent)
                .Include(v => v.Vozac)
                .Include(v => v.Vozilo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (voznje == null) {
                return NotFound();
            }

            return View(voznje);
        }

        // GET: Voznje/Create
        [Authorize(Roles = "Administrator, Korisnik, Vozac, Firma, Klijent")]
        public IActionResult Create() {
            ViewData["firmaId"] = new SelectList(_context.Firma, "id", "id");
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "id");
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "id");
            ViewData["voziloId"] = new SelectList(_context.Vozilo, "id", "id");
            return View();
        }

        // POST: Voznje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,vozacId,korisnikId,firmaId,voziloId,vrijeme,ocjena,cijena,adresaPolazista,adresaDolazista")] Voznje voznje) {
            if (ModelState.IsValid) {
                _context.Add(voznje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["firmaId"] = new SelectList(_context.Firma, "id", "id", voznje.firmaId);
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "id", voznje.korisnikId);
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "id", voznje.vozacId);
            ViewData["voziloId"] = new SelectList(_context.Vozilo, "id", "id", voznje.voziloId);
            return View(voznje);
        }

        // GET: Voznje/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var voznje = await _context.Voznje.FindAsync(id);
            if (voznje == null) {
                return NotFound();
            }
            ViewData["firmaId"] = new SelectList(_context.Firma, "id", "id", voznje.firmaId);
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "id", voznje.korisnikId);
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "id", voznje.vozacId);
            ViewData["voziloId"] = new SelectList(_context.Vozilo, "id", "id", voznje.voziloId);
            return View(voznje);
        }

        // POST: Voznje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,vozacId,korisnikId,firmaId,voziloId,vrijeme,ocjena,cijena,adresaPolazista,adresaDolazista")] Voznje voznje) {
            if (id != voznje.id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(voznje);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!VoznjeExists(voznje.id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["firmaId"] = new SelectList(_context.Firma, "id", "id", voznje.firmaId);
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "id", voznje.korisnikId);
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "id", voznje.vozacId);
            ViewData["voziloId"] = new SelectList(_context.Vozilo, "id", "id", voznje.voziloId);
            return View(voznje);
        }

        // GET: Voznje/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var voznje = await _context.Voznje
                .Include(v => v.Firma)
                .Include(v => v.Klijent)
                .Include(v => v.Vozac)
                .Include(v => v.Vozilo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (voznje == null) {
                return NotFound();
            }

            return View(voznje);
        }

        // POST: Voznje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var voznje = await _context.Voznje.FindAsync(id);
            _context.Voznje.Remove(voznje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoznjeExists(int id) {
            return _context.Voznje.Any(e => e.id == id);
        }
    }
}
