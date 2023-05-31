using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoziMe.Data;
using VoziMe.Models;

namespace VoziMe.Controllers
{
    public class TaxiStajalisteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxiStajalisteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxiStajaliste
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxiStajaliste.ToListAsync());
        }

        // GET: TaxiStajaliste/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiStajaliste = await _context.TaxiStajaliste
                .FirstOrDefaultAsync(m => m.id == id);
            if (taxiStajaliste == null)
            {
                return NotFound();
            }

            return View(taxiStajaliste);
        }

        // GET: TaxiStajaliste/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxiStajaliste/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ime,adresa,brojMjesta")] TaxiStajaliste taxiStajaliste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxiStajaliste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxiStajaliste);
        }

        // GET: TaxiStajaliste/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiStajaliste = await _context.TaxiStajaliste.FindAsync(id);
            if (taxiStajaliste == null)
            {
                return NotFound();
            }
            return View(taxiStajaliste);
        }

        // POST: TaxiStajaliste/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ime,adresa,brojMjesta")] TaxiStajaliste taxiStajaliste)
        {
            if (id != taxiStajaliste.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxiStajaliste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxiStajalisteExists(taxiStajaliste.id))
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
            return View(taxiStajaliste);
        }

        // GET: TaxiStajaliste/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxiStajaliste = await _context.TaxiStajaliste
                .FirstOrDefaultAsync(m => m.id == id);
            if (taxiStajaliste == null)
            {
                return NotFound();
            }

            return View(taxiStajaliste);
        }

        // POST: TaxiStajaliste/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxiStajaliste = await _context.TaxiStajaliste.FindAsync(id);
            _context.TaxiStajaliste.Remove(taxiStajaliste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxiStajalisteExists(int id)
        {
            return _context.TaxiStajaliste.Any(e => e.id == id);
        }
    }
}
