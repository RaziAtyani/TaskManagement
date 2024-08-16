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

        
    }
}

