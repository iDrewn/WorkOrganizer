using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;
using WorkOrganizer.Domain.Services;

namespace WorkOrganizer.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService jobService;

        public JobController(IJobService jobService)
        {

            this.jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var userIdGuid = new Guid(userId);

            var allJobs = await jobService.ListAllJob();

            return View(allJobs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(Job job)
        {
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var IdentityUserId = new Guid(userId);

            var newJob = await jobService.CreateJob(job.Name, job.Description, job.Material, job.Date, job.Hours, job.ProjectId);

            return Created($"{job.Name}, {job.Description}, {job.Material}, {job.Date}, {job.Hours}", newJob);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var deleted = await jobService.DeleteJobAsync(id);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
