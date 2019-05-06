using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Areas.API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId);

  
        Task<Project> GetProjectByTitle(int id, string name);
        Task<Project> EditProject(int projectId, string name, DateTime startDate, string description);
        Task<Project> CreateProject(string name, DateTime startDate, string description, string identityUserId);
        Task<bool> DeleteProject(int id);
    }
}
