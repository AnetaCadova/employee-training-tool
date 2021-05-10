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
        private readonly List<CatalogTask> _listOfTasks;
        private static int _order;

        public LearningPathController(ApplicationDbContext context)
        {
            _context = context;
            _listOfTasks = _context.CatalogTasks.ToList();
            _order = 1;
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
                .Where(task => task.LearningPathId.Equals(learningPath.LearningPathId)).OrderBy(task => task.Order).ToList();

            return View(learningPath);
        }

        // GET: LearningPath/Create
        public IActionResult Create()
        {
            ViewBag.ListOfTasks = _listOfTasks;
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
                        Order = _order,
                        CatalogTaskId = taskId,
                        Description = catalogTask.Description,
                        Title = catalogTask.Title,
                        LearningPathId = learningPath.LearningPathId,
                        TaskType = catalogTask.TaskType
                    };
                    learningPath.Tasks.Add(learningPathTask);
                    _context.LearningPathTasks.Add(learningPathTask);
                    _order++;
                }

                _order = 0;

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

            List<int> selectedTasksId =
                _context.LearningPathTasks.Where(task => task.LearningPathId.Equals(id)).OrderBy(task => task.Order)
                    .Select(task => task.CatalogTaskId).ToList();
            
            List<CatalogTask> catalogTasks = new List<CatalogTask>();
            foreach (var catalogTaskId in selectedTasksId)
            {
                var catalogTask = _listOfTasks.Find(task => task.CatalogTaskId.Equals(catalogTaskId));
                catalogTask.IsSelected = true;
                catalogTasks.Add(catalogTask);
            }

            foreach (var task in _listOfTasks)
            {
                if (!catalogTasks.Contains(task))
                {
                    task.IsSelected = false;
                    catalogTasks.Add(task);
                }
            }

            ViewBag.ListOfTasks = catalogTasks;
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
                    var currentLearningPathTasks = _context.LearningPathTasks.Where(task =>
                        task.LearningPathId.Equals(learningPath.LearningPathId)).ToList();
                    List<int> currentCatalogTasks = currentLearningPathTasks.Select(o => o.CatalogTaskId).ToList();
                    learningPath.Tasks = new List<LearningPathTask>();
                    for (int i = 0; i < tasksToAdd.Count; i++)
                    {
                        var catalogTask = await _context.CatalogTasks.FindAsync(tasksToAdd[i]);
                        var currentLearningPathTask =
                            currentLearningPathTasks.Find(task => task.CatalogTaskId.Equals(catalogTask.CatalogTaskId));
                        if (currentCatalogTasks.Contains(catalogTask.CatalogTaskId) &&
                            currentLearningPathTask.Order.Equals(i + 1))
                        {
                            currentCatalogTasks.Remove(catalogTask.CatalogTaskId);
                            continue;
                        }

                        if (currentCatalogTasks.Contains(catalogTask.CatalogTaskId) &&
                            !currentLearningPathTask.Order.Equals(i + 1))
                        {
                            currentLearningPathTask.Order = i + 1;
                            currentCatalogTasks.Remove(catalogTask.CatalogTaskId);
                            _context.LearningPathTasks.Update(currentLearningPathTask);
                            continue;
                        }

                        var learningPathTask = new LearningPathTask()
                        {
                            CatalogTaskId = catalogTask.CatalogTaskId,
                            Description = catalogTask.Description,
                            Title = catalogTask.Title,
                            LearningPathId = learningPath.LearningPathId,
                            Order = i+1,
                            TaskType = catalogTask.TaskType
                        };
                        learningPath.Tasks.Add(learningPathTask);
                        _context.LearningPathTasks.Add(learningPathTask);
                    }

                    foreach (var task in currentLearningPathTasks)
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