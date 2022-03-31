using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssetMonitorDataAccess.DataAccess;
using AssetMonitorDataAccess.Models;

namespace AspMVC_Monitor.Controllers
{
    public class ConfigTagSharedSetsController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigTagSharedSetsController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigTagSharedSets
        public async Task<IActionResult> Index()
        {
            return View(await _context.TagSharedSet.ToListAsync());
        }

        // GET: ConfigTagSharedSets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagSharedSet = await _context.TagSharedSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagSharedSet == null)
            {
                return NotFound();
            }

            return View(tagSharedSet);
        }

        // GET: ConfigTagSharedSets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConfigTagSharedSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TagSharedSet tagSharedSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tagSharedSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tagSharedSet);
        }

        // GET: ConfigTagSharedSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagSharedSet = await _context.TagSharedSet.FindAsync(id);
            if (tagSharedSet == null)
            {
                return NotFound();
            }
            return View(tagSharedSet);
        }

        // POST: ConfigTagSharedSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TagSharedSet tagSharedSet)
        {
            if (id != tagSharedSet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagSharedSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagSharedSetExists(tagSharedSet.Id))
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
            return View(tagSharedSet);
        }

        // GET: ConfigTagSharedSets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagSharedSet = await _context.TagSharedSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagSharedSet == null)
            {
                return NotFound();
            }

            return View(tagSharedSet);
        }

        // POST: ConfigTagSharedSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagSharedSet = await _context.TagSharedSet.FindAsync(id);
            _context.TagSharedSet.Remove(tagSharedSet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagSharedSetExists(int id)
        {
            return _context.TagSharedSet.Any(e => e.Id == id);
        }
    }
}
