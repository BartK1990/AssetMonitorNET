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
    public class ConfigAssetPropertyValuesController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigAssetPropertyValuesController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigAssetPropertyValues
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.AssetPropertyValue.Include(a => a.Asset).Include(a => a.AssetProperty);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigAssetPropertyValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetPropertyValue = await _context.AssetPropertyValue
                .Include(a => a.Asset)
                .Include(a => a.AssetProperty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetPropertyValue == null)
            {
                return NotFound();
            }

            return View(assetPropertyValue);
        }

        // GET: ConfigAssetPropertyValues/Create
        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress");
            ViewData["AssetPropertyId"] = new SelectList(_context.AssetProperty, "Id", "Name");
            return View();
        }

        // POST: ConfigAssetPropertyValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,AssetPropertyId,AssetId")] AssetPropertyValue assetPropertyValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetPropertyValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", assetPropertyValue.AssetId);
            ViewData["AssetPropertyId"] = new SelectList(_context.AssetProperty, "Id", "Name", assetPropertyValue.AssetPropertyId);
            return View(assetPropertyValue);
        }

        // GET: ConfigAssetPropertyValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetPropertyValue = await _context.AssetPropertyValue.FindAsync(id);
            if (assetPropertyValue == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", assetPropertyValue.AssetId);
            ViewData["AssetPropertyId"] = new SelectList(_context.AssetProperty, "Id", "Name", assetPropertyValue.AssetPropertyId);
            return View(assetPropertyValue);
        }

        // POST: ConfigAssetPropertyValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,AssetPropertyId,AssetId")] AssetPropertyValue assetPropertyValue)
        {
            if (id != assetPropertyValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetPropertyValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetPropertyValueExists(assetPropertyValue.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", assetPropertyValue.AssetId);
            ViewData["AssetPropertyId"] = new SelectList(_context.AssetProperty, "Id", "Name", assetPropertyValue.AssetPropertyId);
            return View(assetPropertyValue);
        }

        // GET: ConfigAssetPropertyValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetPropertyValue = await _context.AssetPropertyValue
                .Include(a => a.Asset)
                .Include(a => a.AssetProperty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetPropertyValue == null)
            {
                return NotFound();
            }

            return View(assetPropertyValue);
        }

        // POST: ConfigAssetPropertyValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetPropertyValue = await _context.AssetPropertyValue.FindAsync(id);
            _context.AssetPropertyValue.Remove(assetPropertyValue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetPropertyValueExists(int id)
        {
            return _context.AssetPropertyValue.Any(e => e.Id == id);
        }
    }
}
