using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Services;

namespace WorkOrganizer.Controllers
{
    public class AdvancedSearchController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IProjectService projectService;
        private readonly IJobService jobService;
        private readonly IApplicationUserService applicationUserService;

        public AdvancedSearchController(IProjectService projectService, IJobService jobService, IApplicationUserService applicationUserService, ApplicationDbContext context)
        {
            this.applicationUserService = applicationUserService;
            this.projectService = projectService;
            this.jobService = jobService;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchProjects(string searchString)
        {
            var searchProjects = await projectService.SearchProjectAsync(searchString);
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(searchProjects);
            }

            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction(nameof(NoResult));
            }

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userIdGuid = new Guid(userId);

            var allProjects = await projectService.GetProjectsByUserId(userIdGuid.ToString());

            return View(allProjects);
        }

        public async Task<IActionResult> SearchUsers(string searchString)
        {
            var searchUsers = await applicationUserService.SearchUser(searchString);
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(searchUsers);
            }

            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction(nameof(NoResult));
            }

            var allProjects = context.ApplicationUsers.ToList();

            return View(allProjects);
        }

        public async Task<IActionResult> SearchJobs(string searchString)
        {
            var searchJobs = await jobService.SearchJob(searchString);
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(searchJobs);
            }

            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction(nameof(NoResult));
            }

            var allProjects = jobService.ListAllJob().ToString();

            return View(allProjects);
        }

        public IActionResult NoResult()
        {
            return View();
        }
    }
}