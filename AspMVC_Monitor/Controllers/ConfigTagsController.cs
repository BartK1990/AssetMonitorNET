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
    public class ConfigTagsController : Controller
    {
        private readonly AssetMonitorContext _context;

        public ConfigTagsController(AssetMonitorContext context)
        {
            _context = context;
        }

        // GET: ConfigTags
        public async Task<IActionResult> Index()
        {
            var assetMonitorContext = _context.Tag.Include(t => t.TagCommunicationRel).Include(t => t.TagSet).Include(t => t.ValueDataType);
            return View(await assetMonitorContext.ToListAsync());
        }

        // GET: ConfigTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .Include(t => t.TagCommunicationRel)
                .Include(t => t.TagSet)
                .Include(t => t.ValueDataType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: ConfigTags/Create
        public IActionResult Create()
        {
            ViewData["TagCommunicationRelId"] = new SelectList(_context.TagCommunicationRel, "Id", "Id");
            ViewData["TagSetId"] = new SelectList(_context.TagSet, "Id", "Name");
            ViewData["ValueDataTypeId"] = new SelectList(_context.TagDataType, "Id", "DataType");
            return View();
        }

        // POST: ConfigTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tagname,ScaleFactor,ScaleOffset,ValueDataTypeId,TagSetId,TagCommunicationRelId")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagCommunicationRelId"] = new SelectList(_context.TagCommunicationRel, "Id", "Id", tag.TagCommunicationRelId);
            ViewData["TagSetId"] = new SelectList(_context.TagSet, "Id", "Name", tag.TagSetId);
            ViewData["ValueDataTypeId"] = new SelectList(_context.TagDataType, "Id", "DataType", tag.ValueDataTypeId);
            return View(tag);
        }

        // GET: ConfigTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            ViewData["TagCommunicationRelId"] = new SelectList(_context.TagCommunicationRel, "Id", "Id", tag.TagCommunicationRelId);
            ViewData["TagSetId"] = new SelectList(_context.TagSet, "Id", "Name", tag.TagSetId);
            ViewData["ValueDataTypeId"] = new SelectList(_context.TagDataType, "Id", "DataType", tag.ValueDataTypeId);
            return View(tag);
        }

        // POST: ConfigTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tagname,ScaleFactor,ScaleOffset,ValueDataTypeId,TagSetId,TagCommunicationRelId")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            ViewData["TagCommunicationRelId"] = new SelectList(_context.TagCommunicationRel, "Id", "Id", tag.TagCommunicationRelId);
            ViewData["TagSetId"] = new SelectList(_context.TagSet, "Id", "Name", tag.TagSetId);
            ViewData["ValueDataTypeId"] = new SelectList(_context.TagDataType, "Id", "DataType", tag.ValueDataTypeId);
            return View(tag);
        }

        // GET: ConfigTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .Include(t => t.TagCommunicationRel)
                .Include(t => t.TagSet)
                .Include(t => t.ValueDataType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: ConfigTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tag.FindAsync(id);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
