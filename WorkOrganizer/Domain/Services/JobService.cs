using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;
using WorkOrganizer.Domain.Repositories;

namespace WorkOrganizer.Domain.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<Job>> ListAllJob()
        {
            return await _jobRepository.GetAsync();
        }

        public async Task<IEnumerable<Job>> ListReportedJobs()
        {
            return await _jobRepository.GetReportedAsync();
        }

        public async Task<Job> CreateJob(string name, string description, string material, DateTime date, string hours, bool isDone)

        {
            return await _jobRepository.CreateJob(name, description, material, date, hours, isDone);
        }
        public async Task<Job> ReportJob(string name, string description, string material, DateTime date, string hours, bool isDone)

        {
            return await _jobRepository.ReportJob(name, description, material, date, hours, isDone);
        }
        public Task<Job> EditJobAsync(int jobId, string name, string description, string material, DateTime date, string hours, bool isDone)
        {
            var job = _jobRepository.EditJobAsync(jobId, name, description, material, date, hours, isDone);
            return job;
        }
        public Task<Job> FindJobByIdAsync(int? id)
        {
            var job = _jobRepository.FindJobById(id);
            return job;
        }

        public async Task<bool> DeleteJobAsync(int? id)
        {
            return await _jobRepository.DeleteJobAsync(id);
        }

        //public Task<IEnumerable<Job>> SearchJobAsync(string searchString)
        //{
        //    var searchJobs = _jobRepository.SearchJob(searchString);
        //    return searchJobs;
        //}
    }
}