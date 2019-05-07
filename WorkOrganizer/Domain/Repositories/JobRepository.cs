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
            var job = await _context.Job.ToListAsync();
            return job;
        }
        //public async Task<IEnumerable<Job>> GetReportedAsync()
        //{
        //    var job = await _context.Job.ToListAsync();
        //    return job;
        //}

        [HttpPost]
        public async Task<Job> CreateJob(string name, string description, string material, DateTime date, string hours, string projectId)
        {
            var newJob = new Job(name, description, material, date, hours, projectId);

            _context.Job.Add(newJob);
            await _context.SaveChangesAsync();
            return newJob;
        }
        public async Task<Job> ReportJob(string name, string description, string material, DateTime date, string hours, string projectId)
        {
            var newJob = new Job(name, description, material, date, hours, projectId);

            _context.Job.Add(newJob);
            await _context.SaveChangesAsync();
            return newJob;
        }

        public async Task<Job> EditJob(int JobId, string name, string description, string material, DateTime date, string hours)
        {
            var updateJob = await _context.Job.FindAsync(JobId);
            updateJob.Name = name;
            updateJob.Description = description;
            updateJob.Material = material;
            updateJob.Date = date;
            updateJob.Hours = hours;

            _context.Job.Update(updateJob);
            await _context.SaveChangesAsync();

            return updateJob;
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
            var searchJobs =  _context.Job.Where(s => s.Name.Contains(searchString));
            return searchJobs;
        }
    }
}