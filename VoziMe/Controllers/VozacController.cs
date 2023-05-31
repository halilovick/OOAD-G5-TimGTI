﻿using System;
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
    public class VozacController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VozacController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vozac
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vozac.ToListAsync());
        }

        // GET: Vozac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozac = await _context.Vozac
                .FirstOrDefaultAsync(m => m.id == id);
            if (vozac == null)
            {
                return NotFound();
            }

            return View(vozac);
        }

        // GET: Vozac/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vozac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("brojVozackeDozvole,ocjena,id,spol,datumRodjenja,ime,prezime,korisnickoIme,lozinka,mailAdresa,adresa,brojTelefona")] Vozac vozac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vozac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vozac);
        }

        // GET: Vozac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozac = await _context.Vozac.FindAsync(id);
            if (vozac == null)
            {
                return NotFound();
            }
            return View(vozac);
        }

        // POST: Vozac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("brojVozackeDozvole,ocjena,id,spol,datumRodjenja,ime,prezime,korisnickoIme,lozinka,mailAdresa,adresa,brojTelefona")] Vozac vozac)
        {
            if (id != vozac.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vozac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VozacExists(vozac.id))
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
            return View(vozac);
        }

        // GET: Vozac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozac = await _context.Vozac
                .FirstOrDefaultAsync(m => m.id == id);
            if (vozac == null)
            {
                return NotFound();
            }

            return View(vozac);
        }

        // POST: Vozac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vozac = await _context.Vozac.FindAsync(id);
            _context.Vozac.Remove(vozac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VozacExists(int id)
        {
            return _context.Vozac.Any(e => e.id == id);
        }
    }
}