using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkOrganizer.Domain.Entities;
using WorkOrganizer.Models;

namespace WorkOrganizer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WorkOrganizer.Domain.Entities.File> File { get; set; }
        public DbSet<ProjectMember> Member { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
    }
}
