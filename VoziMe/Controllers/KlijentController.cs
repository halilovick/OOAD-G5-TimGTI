using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoziMe.Data;
using VoziMe.Models;

namespace VoziMe.Controllers
{
    public class KlijentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static Klijent klijentLokalno;

        public KlijentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Klijent
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klijent.ToListAsync());
        }

        // GET: Klijent/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klijent = await _context.Klijent
                .FirstOrDefaultAsync(m => m.id == id);
            if (klijent == null)
            {
                return NotFound();
            }

            return View(klijent);
        }

        // GET: Klijent/Create
        [Authorize(Roles = "Administrator, Korisnik")]
        public IActionResult Create()
        {
            if(klijentLokalno != null){ // Ukoliko je vec unio podatke
                return RedirectToAction("Edit", "Klijent", new { id = klijentLokalno.id});
            }
            return View();
        }

        // POST: Klijent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Create([Bind("ocjena,id,spol,datumRodjenja,ime,prezime,korisnickoIme,lozinka,mailAdresa,adresa,brojTelefona")] Klijent klijent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klijent);
                await _context.SaveChangesAsync();
                klijentLokalno = klijent;
                //Console.Write(klijentLokalno.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(klijent);
        }

        // GET: Klijent/Edit/5
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klijent = await _context.Klijent.FindAsync(id);
            if (klijent == null)
            {
                return NotFound();
            }
            return View(klijent);
        }

        // POST: Klijent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Korisnik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Edit(int id, [Bind("ocjena,id,spol,datumRodjenja,ime,prezime,korisnickoIme,lozinka,mailAdresa,adresa,brojTelefona")] Klijent klijent)
        {
            if (id != klijent.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klijent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlijentExists(klijent.id))
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
            return View(klijent);
        }

        // GET: Klijent/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klijent = await _context.Klijent
                .FirstOrDefaultAsync(m => m.id == id);
            if (klijent == null)
            {
                return NotFound();
            }

            return View(klijent);
        }

        // POST: Klijent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klijent = await _context.Klijent.FindAsync(id);
            _context.Klijent.Remove(klijent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlijentExists(int id)
        {
            return _context.Klijent.Any(e => e.id == id);
        }
    }
}
