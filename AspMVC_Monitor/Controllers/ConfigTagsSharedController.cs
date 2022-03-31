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
    public class ConfigTagsSharedController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigTagsSharedController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigTagsShared
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.TagShared.Include(t => t.TagSharedSet);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigTagsShared/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagShared = await _context.TagShared
                .Include(t => t.TagSharedSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagShared == null)
            {
                return NotFound();
            }

            return View(tagShared);
        }

        // GET: ConfigTagsShared/Create
        public IActionResult Create()
        {
            ViewData["TagSharedSetId"] = new SelectList(_context.TagSharedSet, "Id", "Name");
            return View();
        }

        // POST: ConfigTagsShared/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tagname,ColumnName,TagSharedSetId,Enable")] TagShared tagShared)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tagShared);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagSharedSetId"] = new SelectList(_context.TagSharedSet, "Id", "Name", tagShared.TagSharedSetId);
            return View(tagShared);
        }

        // GET: ConfigTagsShared/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagShared = await _context.TagShared.FindAsync(id);
            if (tagShared == null)
            {
                return NotFound();
            }
            ViewData["TagSharedSetId"] = new SelectList(_context.TagSharedSet, "Id", "Name", tagShared.TagSharedSetId);
            return View(tagShared);
        }

        // POST: ConfigTagsShared/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tagname,ColumnName,TagSharedSetId,Enable")] TagShared tagShared)
        {
            if (id != tagShared.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagShared);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagSharedExists(tagShared.Id))
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
            ViewData["TagSharedSetId"] = new SelectList(_context.TagSharedSet, "Id", "Name", tagShared.TagSharedSetId);
            return View(tagShared);
        }

        // GET: ConfigTagsShared/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagShared = await _context.TagShared
                .Include(t => t.TagSharedSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tagShared == null)
            {
                return NotFound();
            }

            return View(tagShared);
        }

        // POST: ConfigTagsShared/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagShared = await _context.TagShared.FindAsync(id);
            _context.TagShared.Remove(tagShared);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagSharedExists(int id)
        {
            return _context.TagShared.Any(e => e.Id == id);
        }
    }
}
