using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;
using System.Collections.Generic;
using System;

namespace WorkOrganizer.Domain.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAsync();
        //Task<IEnumerable<Job>> GetReportedAsync();
        Task<Job> CreateJob(string name, string description, string material, DateTime date, string hours, string projectId);
        Task<Job> ReportJob(string name, string description, string material, DateTime date, string hours, string projectId);
        Task<Job> EditJob(int jobId, string name, string description, string material, DateTime date, string hours);
        Task<bool> DeleteJobAsync(int? id);

        //Task<Project> GetByTitle(int id, string name);
        //Task<IEnumerable<Project>> GetAllByUserId(string userId);
        //Task<Project> FindJobById(int? id);
        //Task<Project> UpdateJobById(Project project);
        //Task<Project> JobDetalisByIdAsync(int? id);
        //Task<IEnumerable<Project>> SearchJob(string searchString);
    }
}