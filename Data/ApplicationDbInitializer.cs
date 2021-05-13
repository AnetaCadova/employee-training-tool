using employee_training_tool.Models;
using Microsoft.AspNetCore.Identity;

namespace employee_training_tool.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Password_1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}