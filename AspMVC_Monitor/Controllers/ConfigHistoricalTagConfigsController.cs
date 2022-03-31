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
    public class ConfigHistoricalTagConfigsController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigHistoricalTagConfigsController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigHistoricalTagConfigs
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.HistoricalTagConfig.Include(h => h.HistorizationType).Include(h => h.Tag);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigHistoricalTagConfigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicalTagConfig = await _context.HistoricalTagConfig
                .Include(h => h.HistorizationType)
                .Include(h => h.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historicalTagConfig == null)
            {
                return NotFound();
            }

            return View(historicalTagConfig);
        }

        // GET: ConfigHistoricalTagConfigs/Create
        public IActionResult Create()
        {
            ViewData["HistorizationTypeId"] = new SelectList(_context.HistoricalType, "Id", "Type");
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname");
            return View();
        }

        // POST: ConfigHistoricalTagConfigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TagId,HistorizationTypeId")] HistoricalTagConfig historicalTagConfig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicalTagConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HistorizationTypeId"] = new SelectList(_context.HistoricalType, "Id", "Type", historicalTagConfig.HistorizationTypeId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", historicalTagConfig.TagId);
            return View(historicalTagConfig);
        }

        // GET: ConfigHistoricalTagConfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicalTagConfig = await _context.HistoricalTagConfig.FindAsync(id);
            if (historicalTagConfig == null)
            {
                return NotFound();
            }
            ViewData["HistorizationTypeId"] = new SelectList(_context.HistoricalType, "Id", "Type", historicalTagConfig.HistorizationTypeId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", historicalTagConfig.TagId);
            return View(historicalTagConfig);
        }

        // POST: ConfigHistoricalTagConfigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TagId,HistorizationTypeId")] HistoricalTagConfig historicalTagConfig)
        {
            if (id != historicalTagConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicalTagConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricalTagConfigExists(historicalTagConfig.Id))
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
            ViewData["HistorizationTypeId"] = new SelectList(_context.HistoricalType, "Id", "Type", historicalTagConfig.HistorizationTypeId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", historicalTagConfig.TagId);
            return View(historicalTagConfig);
        }

        // GET: ConfigHistoricalTagConfigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicalTagConfig = await _context.HistoricalTagConfig
                .Include(h => h.HistorizationType)
                .Include(h => h.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historicalTagConfig == null)
            {
                return NotFound();
            }

            return View(historicalTagConfig);
        }

        // POST: ConfigHistoricalTagConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicalTagConfig = await _context.HistoricalTagConfig.FindAsync(id);
            _context.HistoricalTagConfig.Remove(historicalTagConfig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricalTagConfigExists(int id)
        {
            return _context.HistoricalTagConfig.Any(e => e.Id == id);
        }
    }
}
