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
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Job>> GetAsync()
        {
            var job = await _context.Job.FromSql("SELECT * FROM dbo.Job WHERE IsDone LIKE '0%'").ToListAsync();
            return job;
        }
        public async Task<IEnumerable<Job>> GetReportedAsync()
        {
            var reportedJobs = await _context.Job.FromSql("SELECT * FROM dbo.Job WHERE IsDone LIKE '1%'").ToListAsync();
            
            return reportedJobs;
        }

        [HttpPost]
        public async Task<Job> CreateJob(string name, string description, string material, DateTime date, int hours, bool isDone)
        {
            var newJob = new Job(name, description, material, date, hours, isDone);

            _context.Job.Add(newJob);
            await _context.SaveChangesAsync();
            return newJob;
        }
        public async Task<Job> ReportJob(string name, string description, string material, DateTime date, int hours, bool isDone)
        {
            var newJob = new Job(name, description, material, date, hours, isDone);

            _context.Job.Add(newJob);
            await _context.SaveChangesAsync();
            return newJob;
        }

        public async Task<Job> EditJobAsync(int JobId, string name, string description, string material, DateTime date, int hours, bool isDone)
        {
            var updateJob = await _context.Job.FindAsync(JobId);
            updateJob.Name = name;
            updateJob.Description = description;
            updateJob.Material = material;
            updateJob.Date = date;
            updateJob.Hours = hours;
            updateJob.IsDone = isDone;

            _context.Job.Update(updateJob);
            await _context.SaveChangesAsync();

            return updateJob;
        }

        public Task<Job> FindJobById(int? id)
        {
            var job = _context.Job.FirstOrDefaultAsync(x => x.Id == id);

            return job;
        }

        public async Task<bool> DeleteJobAsync(int? id)
        {
            var job = await _context.Job.FindAsync(id);

            if (job == null)
            {
                return await Task.FromResult(false);
            }

            _context.Job.Remove(job);
            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<IEnumerable<Job>> SearchJob(string searchString)
        {
            var searchJobs = _context.Job.Where(s => s.Name.Contains(searchString));
            return searchJobs;
        }
    }
}