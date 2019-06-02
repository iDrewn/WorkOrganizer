using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkOrganizer.Areas.API.Services;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Areas.API.Controllers
{
    [Route("api/projects/{projectId}/jobs")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService jobService;

        public JobController(IJobService jobService)      
        {
            this.jobService = jobService;
        }
        
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsForProject([FromRoute] int projectId) 
        {
            var projectJobs = await jobService.GetJobsByProjectId(projectId);

            if (projectJobs == null)
            {
                return NotFound("No projects");
            }

            return Ok(projectJobs);
        }
    }
}
