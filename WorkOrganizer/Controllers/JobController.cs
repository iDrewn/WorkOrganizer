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
        private readonly ApplicationDbContext _context;

        public JobController(IJobService jobService, ApplicationDbContext context)
        {
            _context = context;
            this.jobService = jobService;
        }

        [HttpGet]
        //public async Task<IEnumerable<Job>> Index(int id)
        //{
        //    var projects = _context.Job.Where(x => x.ProjectId == id);

        //    return await projects.ToListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var loadProject = await _context.Project.Include(x => x.Jobs).FirstOrDefaultAsync(x => x.Id == id);
            if (loadProject == null)
            {
                return NotFound("No projects");
            }
            return View(loadProject);
        }

        [HttpGet]
        public async Task<IActionResult> ReportedJobs()
        {
            var reportedJobs = await jobService.ListReportedJobs();
            return View(reportedJobs);
        }

        [HttpGet]
        public IActionResult CreateJob(int projectId)
        {
            var loadProject =  _context.Project.Include(x => x.Jobs).FirstOrDefaultAsync(x => x.Id == projectId);
            //hämta project och skicka in
            return View(loadProject);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(int projectId, [Bind("Id,Name,Description,Material,Date,Hours,ProjectId,IsDone")] Job job)
        {
            if (ModelState.IsValid)
            {
                var newJob = await jobService.CreateJob(job.Name, job.Description, job.Material, job.Date, job.Hours, projectId, job.IsDone);

                return RedirectToAction(nameof(Index));
            }
            return View(job);

        }

        [HttpGet]
        public async Task<IActionResult> EditJob(int? id)
        {
            var job = await jobService.FindJobByIdAsync(id);

            if (id == null)
            {
                return NotFound();
            }
            return View(job);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditJob(int id, [Bind("Id,Name,Description,Material,Date,Hours,ProjectId,IsDone")] Job job)
        {
            var editJob = await jobService.EditJobAsync(job.Id, job.Name, job.Description, job.Material, job.Date, job.Hours, job.ProjectId, job.IsDone);

            if (editJob != null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteJob(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [HttpPost, ActionName("DeleteJob")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.FindAsync(id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        ////[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteJob(int? id)
        //{
        //    var job = await _context.Job.FindAsync(id);
        //    var deleted = _context.Job.Remove(job);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(DeleteJobConfirmed));

        //    //var deleted = await jobService.DeleteJobAsync(id);

        //    //if (deleted)
        //    //{
        //    //    return NoContent();
        //    //}

        //    //return NotFound();
        //}

        //[HttpPost, ActionName("DeleteJob")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteJobConfirmed(int id)
        //{
        //    var deleteJob = await jobService.DeleteJobAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ProjectExists(int id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}
