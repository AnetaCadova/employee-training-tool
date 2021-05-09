using employee_training_tool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace employee_training_tool.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<AssignedLearningPath> AssignedLearningPaths { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<CatalogTask> CatalogTasks { get; set; }
        public DbSet<LearningPathTask> LearningPathTasks { get; set; }

        public DbSet<AssignedTask> AssignedTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignedLearningPath>()
                .HasOne(path => path.Enrollment)
                .WithOne(e => e.LearningPath)
                .HasForeignKey<Enrollment>(e => e.LearningPathId);
            base.OnModelCreating(modelBuilder);
        }
    }
}