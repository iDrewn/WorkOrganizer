using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Areas.API.Services
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ActionResult<Project>> GetJobsByProjectId(int id)
        {
            var projectJobs = await context.Project
                .Include(x => x.Jobs)
                .FirstOrDefaultAsync(x => x.Id == id);

            return projectJobs;
        }
    }
}
