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
    public class ConfigUserEmailAddressSetsController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigUserEmailAddressSetsController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigUserEmailAddressSets
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserEmailAddressSet.ToListAsync());
        }

        // GET: ConfigUserEmailAddressSets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAddressSet = await _context.UserEmailAddressSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmailAddressSet == null)
            {
                return NotFound();
            }

            return View(userEmailAddressSet);
        }

        // GET: ConfigUserEmailAddressSets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConfigUserEmailAddressSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] UserEmailAddressSet userEmailAddressSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userEmailAddressSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userEmailAddressSet);
        }

        // GET: ConfigUserEmailAddressSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAddressSet = await _context.UserEmailAddressSet.FindAsync(id);
            if (userEmailAddressSet == null)
            {
                return NotFound();
            }
            return View(userEmailAddressSet);
        }

        // POST: ConfigUserEmailAddressSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] UserEmailAddressSet userEmailAddressSet)
        {
            if (id != userEmailAddressSet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEmailAddressSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEmailAddressSetExists(userEmailAddressSet.Id))
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
            return View(userEmailAddressSet);
        }

        // GET: ConfigUserEmailAddressSets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userEmailAddressSet = await _context.UserEmailAddressSet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmailAddressSet == null)
            {
                return NotFound();
            }

            return View(userEmailAddressSet);
        }

        // POST: ConfigUserEmailAddressSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userEmailAddressSet = await _context.UserEmailAddressSet.FindAsync(id);
            _context.UserEmailAddressSet.Remove(userEmailAddressSet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserEmailAddressSetExists(int id)
        {
            return _context.UserEmailAddressSet.Any(e => e.Id == id);
        }
    }
}
