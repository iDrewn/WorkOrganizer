using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Domain.Services
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> ListAllJob();
        Task<IEnumerable<Job>> ListReportedJobs();
        Task<Job> CreateJob(string name, string description, string material, DateTime date, int hours, bool isDone);
        Task<Job> ReportJob(string name, string description, string material, DateTime date, int hours, bool isDone);
        Task<Job> EditJobAsync(int jobId, string name, string description, string material, DateTime date, int hours, bool isDone);
        Task<Job> FindJobByIdAsync(int? id);
        Task<bool> DeleteJobAsync(int? id);
        //Task EditJobAsync(Job job);
        //Task<IEnumerable<Project>> SearchJob(string searchString);
    }
}