using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<User> User { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
