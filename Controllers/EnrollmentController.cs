using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskStatus = employee_training_tool.Models.TaskStatus;

namespace employee_training_tool.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrollment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Enrollments.Include(e => e.LearningPath).Include(e => e.Mentor)
                .Include(e => e.NewComer);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Enrollment/Create
        public IActionResult Create()
        {
            ViewData["LearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId", "Title");
            ViewData["MentorId"] =
                new SelectList(_context.ApplicationUsers.Where(user => user.UserRole.Equals(ApplicationRole.Mentor)),
                    "Id", "UserName");
            ViewData["NewComerId"] =
                new SelectList(_context.ApplicationUsers.Where(user => user.UserRole.Equals(ApplicationRole.Newcomer)),
                    "Id", "UserName");
            return View();
        }

        // POST: Enrollment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,NewComerId,MentorId,LearningPathId")]
            Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollment.Mentor = (User.IsInRole(ApplicationRole.Mentor))
                    ? await _context.ApplicationUsers.FindAsync(
                        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                    : await _context.ApplicationUsers.FindAsync(enrollment.MentorId);
                enrollment.MentorId = enrollment.Mentor.Id;
                enrollment.NewComer = await _context.ApplicationUsers.FindAsync(enrollment.NewComerId);
                var assignedLearningPath = CreateAssignedLearningPath(enrollment);
                enrollment.LearningPathId = assignedLearningPath.AssignedLearningPathId;
                enrollment.LearningPath = assignedLearningPath;

                _context.AssignedLearningPaths.Add(assignedLearningPath);
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                assignedLearningPath.Enrollment = enrollment;
                assignedLearningPath.EnrollmentId = enrollment.EnrollmentId;

                _context.AssignedLearningPaths.Update(assignedLearningPath);
                await _context.SaveChangesAsync();

                if (User.IsInRole(ApplicationRole.Mentor))
                {
                    return Redirect("/AssignedLearningPath/Index");
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["LearningPathId"] = new SelectList(_context.LearningPaths, "LearningPathId", "Title",
                enrollment.LearningPathId);
            ViewData["MentorId"] =
                new SelectList(_context.ApplicationUsers.Where(user => user.UserRole.Equals(ApplicationRole.Mentor)),
                    "Id", "UserName", enrollment.MentorId);
            ViewData["NewComerId"] =
                new SelectList(_context.ApplicationUsers.Where(user => user.UserRole.Equals(ApplicationRole.Newcomer)),
                    "Id", "UserName", enrollment.NewComerId);
            return View(enrollment);
        }

        private AssignedLearningPath CreateAssignedLearningPath(Enrollment enrollment)
        {
            var learningPath = _context.LearningPaths.Find(enrollment.LearningPathId);
            learningPath.Tasks =
                _context.LearningPathTasks.Where(task => task.LearningPathId.Equals(learningPath.LearningPathId))
                    .ToList();
            var assignedLearningPath = new AssignedLearningPath()
            {
                OriginalLearningPathId = learningPath.LearningPathId,
                Description = learningPath.Description,
                Title = learningPath.Title,
                OriginalLearningPath = learningPath,
                MentorId = enrollment.MentorId,
                Mentor = enrollment.Mentor,
                NewComerId = enrollment.NewComerId,
                NewComer = enrollment.NewComer,
                Tasks = new List<AssignedTask>(),
            };
            foreach (var learningPathTask in learningPath.Tasks)
            {
                var assignedTask = new AssignedTask()
                {
                    CatalogTaskId = learningPathTask.CatalogTaskId,
                    AssignedLearningPathId = assignedLearningPath.AssignedLearningPathId,
                    AssignedLearningPath = assignedLearningPath,
                    Description = learningPathTask.Description,
                    Title = learningPathTask.Title,
                    Status = TaskStatus.ToDo,
                    TaskType = learningPathTask.TaskType
                };

                assignedTask.AssignedLearningPathId = assignedLearningPath.AssignedLearningPathId;
                assignedLearningPath.Tasks.Add(assignedTask);
                _context.AssignedTasks.Add(assignedTask);
            }

            return assignedLearningPath;
        }

        // GET: Enrollment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.LearningPath)
                .Include(e => e.Mentor)
                .Include(e => e.NewComer)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            var assignedLearningPath = await _context.AssignedLearningPaths.FindAsync(enrollment.LearningPathId);
            var assignedTasks = _context.AssignedTasks.Where(task =>
                task.AssignedLearningPathId.Equals(assignedLearningPath.AssignedLearningPathId));
            _context.Enrollments.Remove(enrollment);
            _context.AssignedLearningPaths.Remove(assignedLearningPath);
            foreach (var task in assignedTasks)
            {
                _context.AssignedTasks.Remove(task);
            }

            await _context.SaveChangesAsync();
            if (User.IsInRole(ApplicationRole.Mentor))
            {
                return Redirect("/AssignedLearningPath/Index");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}