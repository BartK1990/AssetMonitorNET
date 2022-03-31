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
    public class ConfigApplicationPropertyValuesController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigApplicationPropertyValuesController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigApplicationPropertyValues
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.ApplicationPropertyValue.Include(a => a.ApplicationProperty);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigApplicationPropertyValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationPropertyValue = await _context.ApplicationPropertyValue
                .Include(a => a.ApplicationProperty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationPropertyValue == null)
            {
                return NotFound();
            }

            return View(applicationPropertyValue);
        }

        // GET: ConfigApplicationPropertyValues/Create
        public IActionResult Create()
        {
            ViewData["ApplicationPropertyId"] = new SelectList(_context.ApplicationProperty, "Id", "Name");
            return View();
        }

        // POST: ConfigApplicationPropertyValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,ApplicationPropertyId")] ApplicationPropertyValue applicationPropertyValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationPropertyValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationPropertyId"] = new SelectList(_context.ApplicationProperty, "Id", "Name", applicationPropertyValue.ApplicationPropertyId);
            return View(applicationPropertyValue);
        }

        // GET: ConfigApplicationPropertyValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationPropertyValue = await _context.ApplicationPropertyValue.FindAsync(id);
            if (applicationPropertyValue == null)
            {
                return NotFound();
            }
            ViewData["ApplicationPropertyId"] = new SelectList(_context.ApplicationProperty, "Id", "Name", applicationPropertyValue.ApplicationPropertyId);
            return View(applicationPropertyValue);
        }

        // POST: ConfigApplicationPropertyValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,ApplicationPropertyId")] ApplicationPropertyValue applicationPropertyValue)
        {
            if (id != applicationPropertyValue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationPropertyValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationPropertyValueExists(applicationPropertyValue.Id))
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
            ViewData["ApplicationPropertyId"] = new SelectList(_context.ApplicationProperty, "Id", "Name", applicationPropertyValue.ApplicationPropertyId);
            return View(applicationPropertyValue);
        }

        // GET: ConfigApplicationPropertyValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationPropertyValue = await _context.ApplicationPropertyValue
                .Include(a => a.ApplicationProperty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationPropertyValue == null)
            {
                return NotFound();
            }

            return View(applicationPropertyValue);
        }

        // POST: ConfigApplicationPropertyValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationPropertyValue = await _context.ApplicationPropertyValue.FindAsync(id);
            _context.ApplicationPropertyValue.Remove(applicationPropertyValue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationPropertyValueExists(int id)
        {
            return _context.ApplicationPropertyValue.Any(e => e.Id == id);
        }
    }
}
