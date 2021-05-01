using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskStatus = employee_training_tool.Models.TaskStatus;

namespace EmployeeTrainingTool.Controllers
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
            learningPath.Tasks = _context.AssignedTasks
                .Where(task => task.LearningPathId.Equals(learningPath.LearningPathId)).ToList();
            if (learningPath == null)
            {
                return NotFound();
            }

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
                learningPath.Tasks = new List<AssignedTask>();
                foreach (var taskId in tasksToAdd)
                {
                    var catalogTask = _context.CatalogTasks.Find(taskId);
                    AssignedTask assignedTask = new AssignedTask()
                    {
                        AssignedTaskId = 0, CatalogTaskId = taskId, Description = catalogTask.Description,
                        LearningPathId = learningPath.LearningPathId,
                        Status = TaskStatus.ToDo, TaskType = catalogTask.TaskType
                    };
                    learningPath.Tasks.Add(assignedTask);
                    _context.AssignedTasks.Add(assignedTask);
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

            return View(learningPath);
        }

        // POST: LearningPath/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearningPathId,Title,Description")]
            LearningPath learningPath)
        {
            if (id != learningPath.LearningPathId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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