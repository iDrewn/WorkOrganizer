using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Domain.Services
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser> CreateUserAsync(string email, string password, string name, string firstName, string lastname, string socialSecurityNumber);
    }
}
