using System;
using System.Linq;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace employee_training_tool.Controllers
{
    public class AssignedTaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignedTaskController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: AssignedTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks
                .FirstOrDefaultAsync(m => m.AssignedTaskId == id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            return View(assignedTask);
        }


        // GET: AssignedTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.IsInRole(ApplicationRole.Viewer))
                return BadRequest("Sorry, you are not allowed to access this page.");
            if (id == null)
            {
                return NotFound();
            }

            var assignedTask = await _context.AssignedTasks.FindAsync(id);
            if (assignedTask == null)
            {
                return NotFound();
            }

            ViewData["AssignedLearningPathId"] = new SelectList(_context.AssignedLearningPaths,
                "AssignedLearningPathId", "Title",
                assignedTask.AssignedLearningPathId);
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
            [Bind("AssignedTaskId,AssignedLearningPathId,CatalogTaskId,Title,Description,State,TaskType,Order")]
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

                    var assignedLearningPath = await
                        _context.AssignedLearningPaths.FindAsync(assignedTask.AssignedLearningPathId);
                    var tasks = _context.AssignedTasks.Where(task =>
                        task.AssignedLearningPathId.Equals(assignedLearningPath.AssignedLearningPathId)).ToList();
                    var done = 0;
                    foreach (var task in tasks)
                    {
                        if (task.State.Equals(TaskState.Done))
                        {
                            done++;
                        }
                    }

                    assignedLearningPath.Progress = (double) (Decimal.Divide(done, tasks.Count) * 100);
                    _context.AssignedLearningPaths.Update(assignedLearningPath);
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

                return Redirect("/AssignedLearningPath/Details/" + assignedTask.AssignedLearningPathId);
            }

            ViewData["AssignedLearningPathId"] = new SelectList(_context.AssignedLearningPaths,
                "AssignedLearningPathId", "Title",
                assignedTask.AssignedLearningPathId);
            ViewData["CatalogTaskId"] =
                new SelectList(_context.CatalogTasks, "CatalogTaskId", "Title", assignedTask.CatalogTaskId);
            return View(assignedTask);
        }

        private bool AssignedTaskExists(int id)
        {
            return _context.AssignedTasks.Any(e => e.AssignedTaskId == id);
        }
    }
}