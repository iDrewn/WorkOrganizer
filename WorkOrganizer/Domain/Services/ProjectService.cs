using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkOrganizer.Domain.Entities;
using WorkOrganizer.Domain.Repositories;

namespace WorkOrganizer.Domain.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        
        public async Task<Project> CreateProject(string name, DateTime startDate, DateTime endDate, string description, string identityUserId)

        {
            return await _projectRepository.Create(name, startDate, endDate, description, identityUserId);
        }

        public async Task<bool> DeleteProject(int? id)
        {
            return await _projectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Project>> ListAllProject()
        {
            return await _projectRepository.GetAsync();
        }

        public async Task<Project> GetProjectByTitle(int id, string name)
        {
            return await _projectRepository.GetByTitle(id, name);
        }

        public Task<Project> EditProject(int projectId, string name, DateTime startDate, string description)
        {
            var project = _projectRepository.EditProject(projectId, name, startDate, description);
            return project;
        }

        public Task<IEnumerable<Project>> GetProjectsByUserId(string userId)
        {
            var projects = _projectRepository.GetAllByUserId(userId);

            return projects;
        }

        public Task<Project> FindProjectByIdAsync(int? id)
        {
            var project = _projectRepository.FindProjectById(id);
            return project;
        }

        public Task<Project> UpdateProjectByIdAsync(Project project)
        {
            var pro = _projectRepository.UpdateProjectById(project);
            return pro;
        }

        public Task<Project> ProjectDetalisByIdAsync(int? id)
        {
            var proj = _projectRepository.ProjectDetalisByIdAsync(id);
            return proj;
        }

        public Task<IEnumerable<Project>> SearchProjectAsync(string searchString)
        {
            var searchProjects = _projectRepository.SearchProject(searchString);
            return searchProjects;
        }
    }
}