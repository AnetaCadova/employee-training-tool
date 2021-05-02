using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employee_training_tool.Controllers
{
    public class LearningPathController : Controller
    {
        private readonly ApplicationDbContext _context;
        private List<CatalogTask> ListOfTasks;

        public LearningPathController(ApplicationDbContext context)
        {
            _context = context;
            ListOfTasks = _context.CatalogTasks.ToList();
        }

        // GET: LearningPath
        public async Task<IActionResult> Index()
        {
            return View(await _context.LearningPaths.ToListAsync());
        }

        // GET: LearningPath/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var learningPath = await _context.LearningPaths
                .FirstOrDefaultAsync(m => m.LearningPathId == id);
            learningPath.Tasks = _context.LearningPathTasks
                .Where(task => task.LearningPathId.Equals(learningPath.LearningPathId)).ToList();

            return View(learningPath);
        }

        // GET: LearningPath/Create
        public IActionResult Create()
        {
            ViewBag.ListOfTasks = ListOfTasks;
            return View();
        }

        // POST: LearningPath/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LearningPathId,Title,Description")]
            LearningPath learningPath, [Bind] List<int> tasksToAdd)
        {
            if (ModelState.IsValid)
            {
                learningPath.Tasks = new List<LearningPathTask>();
                foreach (var taskId in tasksToAdd)
                {
                    var catalogTask = await _context.CatalogTasks.FindAsync(taskId);
                    var learningPathTask = new LearningPathTask()
                    {
                        CatalogTaskId = taskId,
                        Description = catalogTask.Description,
                        Title = catalogTask.Title,
                        LearningPathId = learningPath.LearningPathId,
                        TaskType = catalogTask.TaskType
                    };
                    learningPath.Tasks.Add(learningPathTask);
                    _context.LearningPathTasks.Add(learningPathTask);
                }

                _context.Add(learningPath);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(learningPath);
        }

        // GET: LearningPath/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningPath = await _context.LearningPaths.FindAsync(id);
            if (learningPath == null)
            {
                return NotFound();
            }

            ViewBag.ListOfTasks = ListOfTasks;
            return View(learningPath);
        }

        // POST: LearningPath/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearningPathId,Title,Description")]
            LearningPath learningPath, [Bind] List<int> tasksToAdd)
        {
            if (id != learningPath.LearningPathId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentTasks = _context.LearningPathTasks.Where(task =>
                        task.LearningPathId.Equals(learningPath.LearningPathId)).ToList();
                    List<int> currentCatalogTasks = currentTasks.Select(o => o.CatalogTaskId).ToList();
                    learningPath.Tasks = new List<LearningPathTask>();
                    foreach (var taskId in tasksToAdd)
                    {
                        var catalogTask = await _context.CatalogTasks.FindAsync(taskId);
                        if (currentCatalogTasks.Contains(catalogTask.CatalogTaskId))
                        {
                            currentCatalogTasks.Remove(catalogTask.CatalogTaskId);
                            continue;
                        }

                        var learningPathTask = new LearningPathTask()
                        {
                            CatalogTaskId = taskId,
                            Description = catalogTask.Description,
                            Title = catalogTask.Title,
                            LearningPathId = learningPath.LearningPathId,
                            TaskType = catalogTask.TaskType
                        };
                        learningPath.Tasks.Add(learningPathTask);
                        _context.LearningPathTasks.Add(learningPathTask);
                    }

                    foreach (var task in currentTasks)
                    {
                        if (currentCatalogTasks.Contains(task.CatalogTaskId))
                        {
                            _context.LearningPathTasks.Remove(task);
                        }
                    }

                    _context.Update(learningPath);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningPathExists(learningPath.LearningPathId))
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

            return View(learningPath);
        }

        // GET: LearningPath/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningPath = await _context.LearningPaths
                .FirstOrDefaultAsync(m => m.LearningPathId == id);
            if (learningPath == null)
            {
                return NotFound();
            }

            return View(learningPath);
        }

        // POST: LearningPath/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningPath = await _context.LearningPaths.FindAsync(id);
            _context.LearningPaths.Remove(learningPath);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningPathExists(int id)
        {
            return _context.LearningPaths.Any(e => e.LearningPathId == id);
        }
    }
}