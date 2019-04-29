using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;
using WorkOrganizer.Domain.Services;

namespace WorkOrganizer.Areas.API.Controllers
{
    [Route("api/[controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]     //Är detta rätt kod??
                                                                        
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

     
        // GET: api/Projects                        // till en specifik användare!!!
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var listProjects = await projectService.ListAllProject();

            return Ok(listProjects); 
        }







        // GET: api/Projects/5                                         
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id, string name)
        {
            var project = await projectService.GetProjectByTitle(id, name);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);           
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            var newProject = await projectService.EditProject(id, project.Name, project.StartDate, project.Description);

            if (id != project.Id)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            var newProject = await projectService.CreateProject(project.Name, project.StartDate, project.Description);

            return Created($"/api/projects/{newProject.Id}", newProject);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            var deletedProject = await projectService.DeleteProject(id);

            if (deletedProject)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
