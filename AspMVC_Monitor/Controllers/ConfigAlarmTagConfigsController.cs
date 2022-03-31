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
    public class ConfigAlarmTagConfigsController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigAlarmTagConfigsController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigAlarmTagConfigs
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.AlarmTagConfig.Include(a => a.AlarmType).Include(a => a.Tag);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigAlarmTagConfigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarmTagConfig = await _context.AlarmTagConfig
                .Include(a => a.AlarmType)
                .Include(a => a.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alarmTagConfig == null)
            {
                return NotFound();
            }

            return View(alarmTagConfig);
        }

        // GET: ConfigAlarmTagConfigs/Create
        public IActionResult Create()
        {
            ViewData["AlarmTypeId"] = new SelectList(_context.AlarmType, "Id", "Type");
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname");
            return View();
        }

        // POST: ConfigAlarmTagConfigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TagId,AlarmTypeId,ActivationTime,Value,Description")] AlarmTagConfig alarmTagConfig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alarmTagConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlarmTypeId"] = new SelectList(_context.AlarmType, "Id", "Type", alarmTagConfig.AlarmTypeId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", alarmTagConfig.TagId);
            return View(alarmTagConfig);
        }

        // GET: ConfigAlarmTagConfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarmTagConfig = await _context.AlarmTagConfig.FindAsync(id);
            if (alarmTagConfig == null)
            {
                return NotFound();
            }
            ViewData["AlarmTypeId"] = new SelectList(_context.AlarmType, "Id", "Type", alarmTagConfig.AlarmTypeId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", alarmTagConfig.TagId);
            return View(alarmTagConfig);
        }

        // POST: ConfigAlarmTagConfigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TagId,AlarmTypeId,ActivationTime,Value,Description")] AlarmTagConfig alarmTagConfig)
        {
            if (id != alarmTagConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alarmTagConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlarmTagConfigExists(alarmTagConfig.Id))
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
            ViewData["AlarmTypeId"] = new SelectList(_context.AlarmType, "Id", "Type", alarmTagConfig.AlarmTypeId);
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Tagname", alarmTagConfig.TagId);
            return View(alarmTagConfig);
        }

        // GET: ConfigAlarmTagConfigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alarmTagConfig = await _context.AlarmTagConfig
                .Include(a => a.AlarmType)
                .Include(a => a.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alarmTagConfig == null)
            {
                return NotFound();
            }

            return View(alarmTagConfig);
        }

        // POST: ConfigAlarmTagConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alarmTagConfig = await _context.AlarmTagConfig.FindAsync(id);
            _context.AlarmTagConfig.Remove(alarmTagConfig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlarmTagConfigExists(int id)
        {
            return _context.AlarmTagConfig.Any(e => e.Id == id);
        }
    }
}
