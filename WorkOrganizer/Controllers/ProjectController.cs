using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectService projectService;

        public ProjectController(ProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> PostProject(Project project)
        {
            var newProject = await projectService.CreateProject(project.Name, project.StartDate, project.Description);

            return Created($"{project.Name}, {project.StartDate}, {project.Description}", newProject);
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
            var allProjects = await projectService.ListAllProject();

            return Ok(allProjects);
        }

        [HttpGet]
        public async Task<IActionResult> GetById()
        {
            var allProjectsById = await projectService.GetProjectByTitle();

            return Ok(allProjectsById);
        }

        [HttpPut]
        public async Task<IActionResult> PutProject(Project project)
        {
            var newProject = await projectService.EditProject(project.Name, project.StartDate, project.Description);

            return Created($"{project.Name}, {project.StartDate}, {project.Description}", newProject);
        }
    }
}
