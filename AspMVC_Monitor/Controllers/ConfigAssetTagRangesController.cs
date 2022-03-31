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
    public class ConfigAssetTagRangesController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigAssetTagRangesController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigAssetTagRanges
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.AssetTagRange.Include(a => a.Asset).Include(a => a.Tag);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigAssetTagRanges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetTagRange = await _context.AssetTagRange
                .Include(a => a.Asset)
                .Include(a => a.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetTagRange == null)
            {
                return NotFound();
            }

            return View(assetTagRange);
        }

        // GET: ConfigAssetTagRanges/Create
        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress");
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname");
            return View();
        }

        // POST: ConfigAssetTagRanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RangeMax,RangeMin,AssetId,TagId")] AssetTagRange assetTagRange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetTagRange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", assetTagRange.AssetId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", assetTagRange.TagId);
            return View(assetTagRange);
        }

        // GET: ConfigAssetTagRanges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetTagRange = await _context.AssetTagRange.FindAsync(id);
            if (assetTagRange == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", assetTagRange.AssetId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", assetTagRange.TagId);
            return View(assetTagRange);
        }

        // POST: ConfigAssetTagRanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RangeMax,RangeMin,AssetId,TagId")] AssetTagRange assetTagRange)
        {
            if (id != assetTagRange.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetTagRange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetTagRangeExists(assetTagRange.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", assetTagRange.AssetId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", assetTagRange.TagId);
            return View(assetTagRange);
        }

        // GET: ConfigAssetTagRanges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetTagRange = await _context.AssetTagRange
                .Include(a => a.Asset)
                .Include(a => a.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetTagRange == null)
            {
                return NotFound();
            }

            return View(assetTagRange);
        }

        // POST: ConfigAssetTagRanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetTagRange = await _context.AssetTagRange.FindAsync(id);
            _context.AssetTagRange.Remove(assetTagRange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetTagRangeExists(int id)
        {
            return _context.AssetTagRange.Any(e => e.Id == id);
        }
    }
}
