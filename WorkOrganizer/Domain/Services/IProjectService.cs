using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Domain.Services
{
    public interface IProjectService
    {

        Task<Project> CreateProject(string name, DateTime startDate, string description, string identityUserId);
        Task<Project> EditProject(int projectId, string name, DateTime startDate, string description);
        Task<bool> DeleteProject(int? id);
        Task<IEnumerable<Project>> ListAllProject();
        Task<Project> GetProjectByTitle(int id, string name);
        Task<IEnumerable<Project>> GetProjectsByUserId(string userId);
        Task<Project> FindProjectByIdAsync(int? id);
        Task<Project> UpdateProjectByIdAsync(Project project);
        Task<Project> ProjectDetalisByIdAsync(int? id);
    }
}