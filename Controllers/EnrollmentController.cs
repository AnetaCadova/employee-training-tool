using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingTool.Controllers
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

        // GET: Enrollment/Details/5
        public async Task<IActionResult> Details(int? id)
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
                SetEnrollmentValues(enrollment);
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
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

        private void SetEnrollmentValues(Enrollment enrollment)
        {
            enrollment.Mentor = _context.ApplicationUsers.Find(enrollment.MentorId);
            enrollment.NewComer = _context.ApplicationUsers.Find(enrollment.NewComerId);
            LearningPath learningPath = _context.LearningPaths.Find(enrollment.LearningPathId);
            AssignedLearningPath assignedLearningPath = new AssignedLearningPath()
            {
                OriginalLearningPathId = learningPath.LearningPathId,
                Description = learningPath.Description,
                Title = learningPath.Title,
                OriginalLearningPath = learningPath,
                Enrollments = new List<Enrollment>()
            };
            enrollment.LearningPath = assignedLearningPath;
            assignedLearningPath.Enrollments.Add(enrollment);
        }

        // GET: Enrollment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
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

        // POST: Enrollment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,NewComerId,MentorId,LearningPathId")]
            Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SetEnrollmentValues(enrollment);
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
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
                enrollment.LearningPathId);
            ViewData["MentorId"] =
                new SelectList(_context.ApplicationUsers.Where(user => user.UserRole.Equals(ApplicationRole.Mentor)),
                    "Id", "UserName", enrollment.MentorId);
            ViewData["NewComerId"] =
                new SelectList(_context.ApplicationUsers.Where(user => user.UserRole.Equals(ApplicationRole.Newcomer)),
                    "Id", "UserName", enrollment.NewComerId);
            return View(enrollment);
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
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}