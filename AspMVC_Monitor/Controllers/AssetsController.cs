using AspMVC_Monitor.Data.Repositories;
using AspMVC_Monitor.Models;
using AssetMonitorDataAccess.DataAccess;
using AssetMonitorDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AspMVC_Monitor.Controllers
{
    public class AssetsController : Controller
    {
        private readonly AssetMonitorContext _context;
        private readonly IAssetMonitorRepository _repository;
        private readonly IAssetHolder _assetHolder;

        public AssetsController(ILogger<HomeController> logger, IAssetHolder assetHolder, AssetMonitorContext context, IAssetMonitorRepository repository)
        {
            _context = context;
            this._repository = repository;
            this._assetHolder = assetHolder;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAssetsAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _repository.GetAssetByIdAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            ViewData["AssetTypeId"] = new SelectList(_context.Set<AssetType>(), "Id", "Type");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IpAddress,AssetTypeId")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                await _assetHolder.UpdateAssetsListAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetTypeId"] = new SelectList(_context.Set<AssetType>(), "Id", "Type", asset.AssetTypeId);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["AssetTypeId"] = new SelectList(_context.Set<AssetType>(), "Id", "Type", asset.AssetTypeId);
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IpAddress,AssetTypeId")] Asset asset)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                    await _assetHolder.UpdateAssetsListAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.Id))
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
            ViewData["AssetTypeId"] = new SelectList(_context.Set<AssetType>(), "Id", "Type", asset.AssetTypeId);
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.AssetType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            await _assetHolder.UpdateAssetsListAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
