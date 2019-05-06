using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Domain.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Project> Create(string name, DateTime startDate, string description, string identityUserId)
        {
            var newProject = new Project();

            newProject.Name = name;
            newProject.StartDate = startDate;
            newProject.Description = description;
            newProject.IdentityUserId = identityUserId;
            
            _context.Project.Add(newProject);
            await _context.SaveChangesAsync();
            return newProject;
        }

        public async Task<Project> EditProject(int ProjectId,string name,DateTime startDate,string description)
        {
            var updateProject = await _context.Project.FindAsync(ProjectId);
            updateProject.Name = name;
            updateProject.StartDate = startDate;
            updateProject.Description = description;

            _context.Project.Update(updateProject);
            await _context.SaveChangesAsync();

            return updateProject;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return await Task.FromResult(false);
            }

            _context.Project.Remove(project);
            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<IEnumerable<Project>> GetAsync()
        {
            var project = await _context.Project.ToListAsync();
            return project;
        }

        public async Task<Project> GetByTitle(int id, string name)
        {
            return await _context.Project.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Project>> GetAllByUserId(string userId)
        {
            var projects = _context.Project.Where(x => x.IdentityUserId == userId);

            return await projects.ToListAsync();
        }

        

        public Task<Project> FindProjectById(int? id)
        {
            var project = _context.Project.FirstOrDefaultAsync(x => x.Id == id);

            return project;
        }

      
        public async Task<Project> UpdateProjectById(Project project) // ändra fr UpdateProjectById till UpdateProject
        {
            var proj = await _context.Project.FindAsync(project.Id);

            proj.Name = project.Name;
            proj.StartDate = project.StartDate;
            proj.Description = project.Description;

            _context.Project.Update(proj);
            await _context.SaveChangesAsync();

            return proj;
        }

        public Task<Project> ProjectDetalisByIdAsync(int? id)
        {
            var proj = _context.Project.FirstOrDefaultAsync(x => x.Id == id);
            return proj;
        }
    }
}