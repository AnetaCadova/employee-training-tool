using System.Linq;
using employee_training_tool.Models;
using Microsoft.AspNetCore.Identity;

namespace employee_training_tool.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole<int>> _roleManager)
        {
            _roleManager.CreateAsync(new IdentityRole<int>(ApplicationRole.Admin));
            _roleManager.CreateAsync(new IdentityRole<int>(ApplicationRole.Mentor));
            _roleManager.CreateAsync(new IdentityRole<int>(ApplicationRole.Newcomer));
            _roleManager.CreateAsync(new IdentityRole<int>(ApplicationRole.Viewer));
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            AddUser(userManager, "John", "Smith", "admin@gmail.com", "Admin", "Password_1");
            AddUser(userManager, "Allie", "Grater", "newcomer@gmail.com", "Newcomer", "Password_1");
            AddUser(userManager, "Ben", "Dover", "mentor@gmail.com", "Mentor", "Password_1");
        }

        public static void SeedCatalogTasks(ApplicationDbContext context)
        {
            if (context.CatalogTasks.ToList().Count == 0)
            {
                AddCatalogTask(context, "Create gmail labels", "Please create gmail labels.", TaskTypes.Exercise, 1);
                AddCatalogTask(context, "Generate tokens", "Please generate your tokens.", TaskTypes.Exercise, 1);
                AddCatalogTask(context, "Explore your timesheet",
                    "Check your timesheet and try to log time spent at work today.", TaskTypes.Exercise, 1);
                AddCatalogTask(context, "Company strategy",
                    "Watch this video about the culture and strategy of our company.", TaskTypes.Video, 1);
                AddCatalogTask(context, "Explore your role",
                    "Read about your role in the team. Meet your new responsibilities.", TaskTypes.Lecture, 1);
            }
        }

        private static void AddUser(UserManager<ApplicationUser> userManager, string firstName, string lastName,
            string email, string role, string password)
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    Email = email,
                    UserRole = role
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }

        private static void AddCatalogTask(ApplicationDbContext context, string title, string description,
            TaskTypes type, int authorId)
        {
            CatalogTask task = new CatalogTask()
            {
                Title = title,
                Description = description,
                TaskType = type,
                AuthorId = authorId,
            };

            context.CatalogTasks.Add(task);
            context.SaveChangesAsync();
        }
    }
}