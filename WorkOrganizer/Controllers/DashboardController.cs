using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WorkOrganizer.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ProMan.Controllers
{
    [Authorize]
    //[Authorize(Roles = SD.SuperAdminEndUser)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext context;

        private readonly IProjectService projectService;

        public DashboardController(IProjectService projectService, ApplicationDbContext context)
        {

            this.projectService = projectService;
            this.context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            return View(await projectService.ListAllProject());
        }

        // GET: Dashboard/Projects
        public async Task<IActionResult> Projects(string searchString)
        {
            var searchProjects = await projectService.SearchProjectAsync(searchString);
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(searchProjects);
            }

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userIdGuid = new Guid(userId);

            var allProjects = await projectService.GetProjectsByUserId(userIdGuid.ToString());

            return View(allProjects);
        }

        // GET: Dashboard/ProjectDetails/5
        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proj = await projectService.ProjectDetalisByIdAsync(id);

            return View(proj);

        }

        // GET: Dashboard/ProjectCreate
        public IActionResult CreateProject()
        {
            return View();
        }
  
        // POST: Dashboard/CreateProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject([Bind("Id,Name,StartDate,Description,IdentityUserId")] Project project) 
        {
            if (ModelState.IsValid)
            {                
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var newProject = projectService.CreateProject(project.Name, project.StartDate, project.EndDate, project.Description, userId);

                return RedirectToAction(nameof(Projects));
            }
            return View(project);
        } 

        // GET: Dashboard/EditProject/5
        public async Task<IActionResult> EditProject(int? id)
        {
            var project = await projectService.FindProjectByIdAsync(id);

            if(id == null)
            {
                return NotFound();
            }
            return View(project);
            
        }

        // POST: Dashboard/EditProject/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int id, [Bind("Id,Name,StartDate,Description,IdentityUserId")] Project project)
        {
            var projectUpdate = await projectService.UpdateProjectByIdAsync(project);

            if(projectUpdate != null)
            {
                return RedirectToAction(nameof(Projects));
            } 
            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await context.Project
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await context.Project.FindAsync(id);
            context.Project.Remove(project);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return context.Project.Any(e => e.Id == id);
        }
    }
}
