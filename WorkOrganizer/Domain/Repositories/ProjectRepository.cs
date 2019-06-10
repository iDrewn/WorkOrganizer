using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        
        public async Task<Project> Create(string name, DateTime startDate, DateTime endDate, string description, string identityUserId)
        {
            var newProject = new Project(name, startDate, endDate, description, identityUserId);

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
            var allProjects = new List<Project>();

            var ownProjects = _context.Project.Where(x => x.IdentityUserId == userId).ToList();
            var memberProjects = _context.Member.Where(x => x.MemberId == userId).Include(x => x.Project).Select(x => x.Project).ToList();

            //allProjects.AddRange(ownProjects);
            allProjects.AddRange(memberProjects);

            return allProjects;
        }

        

        public Task<Project> FindProjectById(int? id)
        {
            var project = _context.Project.FirstOrDefaultAsync(x => x.Id == id);

            return project;
        }

      
        public async Task<Project> UpdateProjectById(Project project)
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
            var proj = _context.Project
                
                .Include(x => x.Files)
                .FirstOrDefaultAsync(x => x.Id == id);
            return proj;
        }

        public async Task<IEnumerable<Project>> SearchProject(string searchString)
        {
            var searchProjects = _context.Project.Where(s => s.Name.Contains(searchString));
            return searchProjects;
        }
    }
}