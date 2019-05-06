﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Areas.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext context;

        public ProjectService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Task<Project> CreateProject(string name, DateTime startDate, string description, string identityUser)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProject(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> EditProject(int projectId, string name, DateTime startDate, string description)
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetProjectByTitle(int id, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(string userId)
        {
            var projects = context.Project.Where(x => x.IdentityUserId == userId);

            return await projects.ToListAsync(); 
        }
    }
}
