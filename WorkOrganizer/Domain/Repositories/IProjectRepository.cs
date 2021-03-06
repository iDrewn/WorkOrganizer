using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;
using System.Collections.Generic;
using System;

namespace WorkOrganizer.Domain.Repositories
{
    public interface IProjectRepository
    {

        
        Task<Project> EditProject(int projectId, string name, DateTime startDate, string description);
        Task<bool> DeleteAsync(int? id);
        Task<IEnumerable<Project>> GetAsync();
        Task<Project> GetByTitle(int id, string name);
        Task<IEnumerable<Project>> GetAllByUserId(string userId);
        Task<Project> Create(string name, DateTime startDate, DateTime endDate, string description, string identityUserId);
        Task<Project> FindProjectById(int? id);
        Task<Project> UpdateProjectById(Project project);
        Task<Project> ProjectDetalisByIdAsync(int? id);
        Task<IEnumerable<Project>> SearchProject(string searchString);
    }
}