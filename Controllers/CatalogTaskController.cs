using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employee_training_tool.Controllers
{
    public class CatalogTaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CatalogTask
        public async Task<IActionResult> Index()
        {
            return View(await _context.CatalogTasks.ToListAsync());
        }

        // GET: CatalogTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogTask = await _context.CatalogTasks
                .FirstOrDefaultAsync(m => m.CatalogTaskId == id);
            if (catalogTask == null)
            {
                return NotFound();
            }

            return View(catalogTask);
        }

        // GET: CatalogTask/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CatalogTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogTaskId,Title,Description,TaskType")]
            CatalogTask catalogTask)
        {
            if (ModelState.IsValid)
            {
                catalogTask.Author = await _context.ApplicationUsers.FindAsync(
                    int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                catalogTask.AuthorId = catalogTask.Author.Id;
                _context.Add(catalogTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(catalogTask);
        }

        // GET: CatalogTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogTask = await _context.CatalogTasks.FindAsync(id);
            if (catalogTask == null)
            {
                return NotFound();
            }

            return View(catalogTask);
        }

        // POST: CatalogTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatalogTaskId,Title,Description,TaskType, AuthorId")]
            CatalogTask catalogTask)
        {
            if (id != catalogTask.CatalogTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogTaskExists(catalogTask.CatalogTaskId))
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

            return View(catalogTask);
        }

        // GET: CatalogTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogTask = await _context.CatalogTasks
                .FirstOrDefaultAsync(m => m.CatalogTaskId == id);
            if (catalogTask == null)
            {
                return NotFound();
            }

            return View(catalogTask);
        }

        // POST: CatalogTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogTask = await _context.CatalogTasks.FindAsync(id);
            _context.CatalogTasks.Remove(catalogTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogTaskExists(int id)
        {
            return _context.CatalogTasks.Any(e => e.CatalogTaskId == id);
        }
    }
}