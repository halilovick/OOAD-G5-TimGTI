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

namespace VoziMe.Controllers
{
    public class VoznjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoznjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Voznje
        [Authorize(Roles = "Administrator, Korisnik, Vozac, Firma, Klijent")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Voznje.Where(v=>v.korisnikId == KlijentController.klijentLokalno.id).Include(v => v.Firma).Include(v => v.Klijent).Include(v => v.Vozac).Include(v => v.Vozilo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Voznje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznje = await _context.Voznje
                .Include(v => v.Firma)
                .Include(v => v.Klijent)
                .Include(v => v.Vozac)
                .Include(v => v.Vozilo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (voznje == null)
            {
                return NotFound();
            }

            return View(voznje);
        }

        // GET: Voznje/Create
        [Authorize(Roles = "Administrator, Korisnik, Vozac, Firma, Klijent")]
        public IActionResult Create()
        {
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
        public async Task<IActionResult> Create([Bind("id,vozacId,korisnikId,firmaId,voziloId,vrijeme,ocjena,cijena,adresaPolazista,adresaDolazista")] Voznje voznje)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("id,vozacId,korisnikId,firmaId,voziloId,vrijeme,ocjena,cijena,adresaPolazista,adresaDolazista")] Voznje voznje)
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
            ViewData["firmaId"] = new SelectList(_context.Firma, "id", "id", voznje.firmaId);
            ViewData["korisnikId"] = new SelectList(_context.Klijent, "id", "id", voznje.korisnikId);
            ViewData["vozacId"] = new SelectList(_context.Vozac, "id", "id", voznje.vozacId);
            ViewData["voziloId"] = new SelectList(_context.Vozilo, "id", "id", voznje.voziloId);
            return View(voznje);
        }

        // GET: Voznje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voznje = await _context.Voznje
                .Include(v => v.Firma)
                .Include(v => v.Klijent)
                .Include(v => v.Vozac)
                .Include(v => v.Vozilo)
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
