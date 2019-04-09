using System;
using System.Collections.Generic;
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


        [HttpPost]
        public async Task<Project> CreateProject(string name, DateTime startDate, string description)
        {
            return await _projectRepository.CreateAsync(name, startDate, description);
        }
        public async Task<bool> DeleteProject(int id)
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
    }
}