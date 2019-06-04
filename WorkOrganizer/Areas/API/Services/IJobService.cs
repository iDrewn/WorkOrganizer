using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Areas.API.Services
{
    public interface IJobService
    {
        Task<ActionResult<Project>> GetJobsByProjectId(int id);
    }
}
