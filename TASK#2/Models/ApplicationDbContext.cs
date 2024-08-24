using Microsoft.EntityFrameworkCore;
using TASK_2.models;

namespace TASK_2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Registrations> Registration { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure self-referencing relationship for Project
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ParentProject)
                .WithMany(p => p.SubProjects)
                .HasForeignKey(p => p.ParentProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship between Project and Registration
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Registration)
                .WithMany()  // Remove the Projects collection reference
                .HasForeignKey(p => p.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
