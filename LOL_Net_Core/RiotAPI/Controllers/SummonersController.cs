using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RiotAPI.Models;

namespace RiotAPI.Controllers
{
    public class SummonersController : Controller
    {
        private readonly RiotAPIContext _context;

        public SummonersController(RiotAPIContext context)
        {
            _context = context;    
        }

        // GET: Summoners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Summoner.ToListAsync());
        }

        // GET: Summoners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summoner = await _context.Summoner
                .SingleOrDefaultAsync(m => m.ID == id);
            if (summoner == null)
            {
                return NotFound();
            }

            return View(summoner);
        }

        // GET: Summoners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Summoners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,IrlName,Rank")] Summoner summoner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(summoner);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(summoner);
        }

        // GET: Summoners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summoner = await _context.Summoner.SingleOrDefaultAsync(m => m.ID == id);
            if (summoner == null)
            {
                return NotFound();
            }
            return View(summoner);
        }

        // POST: Summoners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,IrlName,Rank")] Summoner summoner)
        {
            if (id != summoner.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(summoner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SummonerExists(summoner.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(summoner);
        }

        // GET: Summoners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summoner = await _context.Summoner
                .SingleOrDefaultAsync(m => m.ID == id);
            if (summoner == null)
            {
                return NotFound();
            }

            return View(summoner);
        }

        // POST: Summoners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var summoner = await _context.Summoner.SingleOrDefaultAsync(m => m.ID == id);
            _context.Summoner.Remove(summoner);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SummonerExists(int id)
        {
            return _context.Summoner.Any(e => e.ID == id);
        }
    }
}
