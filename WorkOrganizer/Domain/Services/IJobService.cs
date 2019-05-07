using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Domain.Services
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> ListAllJob();
        //Task<IEnumerable<Job>> ListReportedJobs();
        Task<Job> CreateJob(string name, string description, string material, DateTime date, string hours, string projectId);
        Task<Job> ReportJob(string name, string description, string material, DateTime date, string hours, string projectId);
        Task<Job> EditJob(int jobId, string name, string description, string material, DateTime date, string hours);
        Task<bool> DeleteJobAsync(int? id);
        //Task<IEnumerable<Project>> SearchJob(string searchString);
    }
}