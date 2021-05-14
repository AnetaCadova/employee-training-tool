using System.Linq;
using System.Threading.Tasks;
using employee_training_tool.Data;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace employee_training_tool.Controllers
{
    public class AdminPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPage
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole(ApplicationRole.Admin))
                return BadRequest("Sorry, you are not allowed to access this page.");

            return View(await _context.ApplicationUsers.ToListAsync());
        }

        // GET: AdminPage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.IsInRole(ApplicationRole.Admin))
                return BadRequest("Sorry, you are not allowed to access this page.");

            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: AdminPage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.IsInRole(ApplicationRole.Admin))
                return BadRequest("Sorry, you are not allowed to access this page.");

            ViewData["Roles"] = ApplicationRole.getRoles()
                .Select(role =>
                    new SelectListItem
                    {
                        Value = role.ToString(),
                        Text = role
                    });
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: AdminPage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind(
                "FirstName,LastName,UserRole,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")]
            ApplicationUser applicationUser)
        {
            if (!User.IsInRole(ApplicationRole.Admin))
                return BadRequest("Sorry, you are not allowed to access this page.");
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationUser.NormalizedEmail = applicationUser.Email.ToUpper();
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
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

            return View(applicationUser);
        }

        // GET: AdminPage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole(ApplicationRole.Admin))
                return BadRequest("Sorry, you are not allowed to access this page.");

            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: AdminPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole(ApplicationRole.Admin))
                return BadRequest("Sorry, you are not allowed to access this page.");

            var applicationUser = await _context.ApplicationUsers.FindAsync(id);
            _context.ApplicationUsers.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}