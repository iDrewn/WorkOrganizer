using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkOrganizer.Domain.Entities;
using WorkOrganizer.Models;

namespace WorkOrganizer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Project> Project { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } // add
    }
}
