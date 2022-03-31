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
    public class ConfigUserEmailAssetRelsController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigUserEmailAssetRelsController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigUserEmailAssetRels
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.UserEmailAssetRel.Include(u => u.Asset).Include(u => u.UserEmailAddressSet);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigUserEmailAssetRels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAssetRel = await _context.UserEmailAssetRel
                .Include(u => u.Asset)
                .Include(u => u.UserEmailAddressSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmailAssetRel == null)
            {
                return NotFound();
            }

            return View(userEmailAssetRel);
        }

        // GET: ConfigUserEmailAssetRels/Create
        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress");
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name");
            return View();
        }

        // POST: ConfigUserEmailAssetRels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssetId,UserEmailAddressSetId")] UserEmailAssetRel userEmailAssetRel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userEmailAssetRel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", userEmailAssetRel.AssetId);
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name", userEmailAssetRel.UserEmailAddressSetId);
            return View(userEmailAssetRel);
        }

        // GET: ConfigUserEmailAssetRels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAssetRel = await _context.UserEmailAssetRel.FindAsync(id);
            if (userEmailAssetRel == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", userEmailAssetRel.AssetId);
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name", userEmailAssetRel.UserEmailAddressSetId);
            return View(userEmailAssetRel);
        }

        // POST: ConfigUserEmailAssetRels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetId,UserEmailAddressSetId")] UserEmailAssetRel userEmailAssetRel)
        {
            if (id != userEmailAssetRel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEmailAssetRel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEmailAssetRelExists(userEmailAssetRel.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Asset, "Id", "IpAddress", userEmailAssetRel.AssetId);
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name", userEmailAssetRel.UserEmailAddressSetId);
            return View(userEmailAssetRel);
        }

        // GET: ConfigUserEmailAssetRels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAssetRel = await _context.UserEmailAssetRel
                .Include(u => u.Asset)
                .Include(u => u.UserEmailAddressSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmailAssetRel == null)
            {
                return NotFound();
            }

            return View(userEmailAssetRel);
        }

        // POST: ConfigUserEmailAssetRels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userEmailAssetRel = await _context.UserEmailAssetRel.FindAsync(id);
            _context.UserEmailAssetRel.Remove(userEmailAssetRel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserEmailAssetRelExists(int id)
        {
            return _context.UserEmailAssetRel.Any(e => e.Id == id);
        }
    }
}
