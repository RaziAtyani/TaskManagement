using Microsoft.EntityFrameworkCore;
using TASK_2.models;

namespace TASK_2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed predefined roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "ProjectLead" },
                new Role { Id = 3, Name = "Member" }
            );

            // Configure self-referencing relationship for Project
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ParentProject)
                .WithMany(p => p.SubProjects)
                .HasForeignKey(p => p.ParentProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship between Project and Registration
            modelBuilder.Entity<Project>()
                .HasOne(p => p.Registration)
                .WithMany(r => r.Projects)
                .HasForeignKey(p => p.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship between Team and Project
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Teams)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship between TeamMember and Team
            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Team)
                .WithMany(t => t.TeamMembers)
                .HasForeignKey(tm => tm.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship between TeamMember and User
            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.User)
                .WithMany(u => u.TeamMembers)
                .HasForeignKey(tm => tm.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure relationship between TeamMember and Role
            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Role)
                .WithMany(r => r.TeamMembers)
                .HasForeignKey(tm => tm.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure relationship between Task and Project
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship between Task and User (AssignedTo)
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure self-referencing relationship for Task (Subtasks)
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.ParentTask)
                .WithMany(t => t.SubTasks)
                .HasForeignKey(t => t.ParentTaskId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
