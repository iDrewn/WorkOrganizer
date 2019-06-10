using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkOrganizer.Domain.Entities;
using WorkOrganizer.Domain.Services;

namespace WorkOrganizer.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        public IActionResult PostProject(Project project)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var IdentityUserId = new Guid(userId);

            var newProject = projectService.CreateProject(project.Name, project.StartDate, project.EndDate, project.Description, IdentityUserId.ToString()); 

            return Created($"{project.Name}, {project.StartDate}, {project.Description}, {project.IdentityUserId}", newProject); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveProject(int id)
        {
            var deleted = await projectService.DeleteProject(id);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userIdGuid = new Guid(userId);
            
            var allProjects = await projectService.GetProjectsByUserId(userIdGuid.ToString());

            return Ok(allProjects);
        }
    }
}
