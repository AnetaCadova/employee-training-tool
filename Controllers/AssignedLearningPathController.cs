using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingTool.Controllers
{
    public class AssignedLearningPathController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignedLearningPathController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignedLearningPath
        public async Task<IActionResult> Index()
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var applicationDbContext = _context.AssignedLearningPaths.Where(path =>
                    path.MentorId.Equals(currentUserId) || path.NewComerId.Equals(currentUserId)).Include(e => e.Mentor)
                .Include(e => e.NewComer);
            ;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssignedLearningPath/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedLearningPath = await _context.AssignedLearningPaths.Include(e => e.Mentor)
                .Include(e => e.NewComer)
                .FirstOrDefaultAsync(m => m.AssignedLearningPathId == id);
            assignedLearningPath.Tasks = _context.AssignedTasks
                .Where(task => task.AssignedLearningPathId.Equals(assignedLearningPath.AssignedLearningPathId))
                .ToList();
            if (assignedLearningPath == null)
            {
                return NotFound();
            }

            return View(assignedLearningPath);
        }

        // GET: AssignedLearningPath/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignedLearningPath = await _context.AssignedLearningPaths.FindAsync(id);
            if (assignedLearningPath == null)
            {
                return NotFound();
            }

            ViewData["OriginalLearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId",
                "LearningPathId", assignedLearningPath.OriginalLearningPathId);
            return View(assignedLearningPath);
        }

        // POST: AssignedLearningPath/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("AssignedLearningPathId,OriginalLearningPathId,MentorId,NewComerId,Title,Description")]
            AssignedLearningPath assignedLearningPath)
        {
            if (id != assignedLearningPath.AssignedLearningPathId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignedLearningPath);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignedLearningPathExists(assignedLearningPath.AssignedLearningPathId))
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

            ViewData["OriginalLearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId",
                "LearningPathId", assignedLearningPath.OriginalLearningPathId);
            return View(assignedLearningPath);
        }
        private bool AssignedLearningPathExists(int id)
        {
            return _context.AssignedLearningPaths.Any(e => e.AssignedLearningPathId == id);
        }
    }
}