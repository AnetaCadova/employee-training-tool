using employee_training_tool.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace employee_training_tool.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<AssignedLearningPath> AssignedLearningPaths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Newcomer> NewComers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}