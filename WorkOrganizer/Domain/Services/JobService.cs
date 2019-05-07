using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        //public async Task<IEnumerable<Job>> ListReportedJobs()
        //{
        //    return await _jobRepository.GetReportedAsync();
        //}

        public async Task<Job> CreateJob(string name, string description, string material, DateTime date, string hours, string projectId)

        {
            return await _jobRepository.CreateJob(name, description, material, date, hours, projectId);
        }
        public async Task<Job> ReportJob(string name, string description, string material, DateTime date, string hours, string projectId)

        {
            return await _jobRepository.ReportJob(name, description, material, date, hours, projectId);
        }
        public Task<Job> EditJob(int jobId, string name, string description, string material, DateTime date, string hours)
        {
            var job = _jobRepository.EditJob(jobId, name, description, material, date, hours);
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