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
    public class ConfigUserEmailAddressesController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigUserEmailAddressesController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigUserEmailAddresses
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.UserEmailAddress.Include(u => u.UserEmailAddressSet);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigUserEmailAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAddress = await _context.UserEmailAddress
                .Include(u => u.UserEmailAddressSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmailAddress == null)
            {
                return NotFound();
            }

            return View(userEmailAddress);
        }

        // GET: ConfigUserEmailAddresses/Create
        public IActionResult Create()
        {
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name");
            return View();
        }

        // POST: ConfigUserEmailAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,UserEmailAddressSetId")] UserEmailAddress userEmailAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userEmailAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name", userEmailAddress.UserEmailAddressSetId);
            return View(userEmailAddress);
        }

        // GET: ConfigUserEmailAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAddress = await _context.UserEmailAddress.FindAsync(id);
            if (userEmailAddress == null)
            {
                return NotFound();
            }
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name", userEmailAddress.UserEmailAddressSetId);
            return View(userEmailAddress);
        }

        // POST: ConfigUserEmailAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,UserEmailAddressSetId")] UserEmailAddress userEmailAddress)
        {
            if (id != userEmailAddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEmailAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEmailAddressExists(userEmailAddress.Id))
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
            ViewData["UserEmailAddressSetId"] = new SelectList(_context.UserEmailAddressSet, "Id", "Name", userEmailAddress.UserEmailAddressSetId);
            return View(userEmailAddress);
        }

        // GET: ConfigUserEmailAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAddress = await _context.UserEmailAddress
                .Include(u => u.UserEmailAddressSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmailAddress == null)
            {
                return NotFound();
            }

            return View(userEmailAddress);
        }

        // POST: ConfigUserEmailAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userEmailAddress = await _context.UserEmailAddress.FindAsync(id);
            _context.UserEmailAddress.Remove(userEmailAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserEmailAddressExists(int id)
        {
            return _context.UserEmailAddress.Any(e => e.Id == id);
        }
    }
}
