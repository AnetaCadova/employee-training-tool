using System.Linq;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskStatus = employee_training_tool.Models.TaskStatus;

namespace EmployeeTrainingTool.Controllers
{
    public class AssignedTaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignedTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignedTask
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssignedTasks.Include(a => a.LearningPath);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssignedTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks
                .Include(a => a.LearningPath)
                .FirstOrDefaultAsync(m => m.AssignedTaskId == id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            return View(assignedTask);
        }

        // GET: AssignedTask/Create
        public IActionResult Create()
        {
            ViewData["LearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId", "Title");
            ViewData["CatalogTaskId"] = new SelectList(_context.CatalogTasks, "CatalogTaskId", "Title");
            return View();
        }

        // POST: AssignedTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("AssignedTaskId,LearningPathId,CatalogTaskId")]
            AssignedTask assignedTask)
        {
            if (ModelState.IsValid)
            {
                var catalogTask = _context.CatalogTasks.Find(assignedTask.CatalogTaskId);
                var learningPath = _context.LearningPaths.Find(assignedTask.LearningPathId);
                assignedTask.Description = catalogTask.Description;
                assignedTask.Title = catalogTask.Title;
                assignedTask.TaskType = catalogTask.TaskType;
                assignedTask.Status = TaskStatus.ToDo;
                assignedTask.LearningPath = learningPath;

                _context.Add(assignedTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId", "Title",
                assignedTask.LearningPathId);
            ViewData["CatalogTaskId"] =
                new SelectList(_context.CatalogTasks, "CatalogTaskId", "Title", assignedTask.CatalogTaskId);
            return View(assignedTask);
        }

        // GET: AssignedTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            ViewData["LearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId", "Title",
                assignedTask.LearningPathId);
            ViewData["CatalogTaskId"] =
                new SelectList(_context.CatalogTasks, "CatalogTaskId", "Title", assignedTask.CatalogTaskId);
            return View(assignedTask);
        }

        // POST: AssignedTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("AssignedTaskId,LearningPathId,CatalogTaskId,Title,Description,Status,TaskType")]
            AssignedTask assignedTask)
        {
            if (id != assignedTask.AssignedTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignedTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignedTaskExists(assignedTask.AssignedTaskId))
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

            ViewData["LearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId", "Title",
                assignedTask.LearningPathId);
            ViewData["CatalogTaskId"] =
                new SelectList(_context.CatalogTasks, "CatalogTaskId", "Title", assignedTask.CatalogTaskId);
            return View(assignedTask);
        }

        // GET: AssignedTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks
                .Include(a => a.LearningPath)
                .FirstOrDefaultAsync(m => m.AssignedTaskId == id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            return View(assignedTask);
        }

        // POST: AssignedTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            _context.AssignedTasks.Remove(assignedTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignedTaskExists(int id)
        {
            return _context.AssignedTasks.Any(e => e.AssignedTaskId == id);
        }
    }
}