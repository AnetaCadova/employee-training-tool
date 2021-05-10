using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using employee_training_tool.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace employee_training_tool.Controllers
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

            return View(assignedLearningPath);
        }
    }
}