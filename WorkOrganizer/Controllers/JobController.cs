using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(int id)
        {
            var loadProject = await _context.Project
                .Include(x => x.Jobs)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (loadProject == null)
            {
                return NotFound("No jobs");
            }

            return View(loadProject);
        }

        [HttpGet]
        public IActionResult ReportedJobs(int id)
        {
            var project = _context.Project.FirstOrDefault(x => x.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var reportedJobs = _context.Job
                .FromSql($"SELECT * FROM dbo.Job WHERE IsDone LIKE '1%'")
                .Where(b => b.ProjectId == id)
                .ToList();

            var reportedTime = _context.Job
                .FromSql("SELECT * FROM dbo.Job WHERE IsDone LIKE '1%'")
                .Where(x => x.ProjectId == id)
                .Sum(b => b.Hours)
                .ToString();

            ViewBag.ReportedTime = reportedTime;
            ViewBag.ReportedJobs = reportedJobs;

            return View(project);
        }

        [HttpGet]
        public IActionResult CreateJob(int projectId)
        {
            var job = new Job { ProjectId = projectId };

            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(Job job)
        {
            if (ModelState.IsValid)
            {
                await _context.Job.AddAsync(job);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { id = job.ProjectId });
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
        public async Task<IActionResult> EditJob(int id, Job job)
        {
            var editJob = await jobService.EditJobAsync(job.Id, job.Name, job.Description, job.Material, job.Date, job.Hours, job.IsDone);

            if (editJob != null)
            {
                return RedirectToAction(nameof(Index), new { id = job.ProjectId });
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

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Projects", "Dashboard");
        }

        private bool ProjectExists(int id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}
