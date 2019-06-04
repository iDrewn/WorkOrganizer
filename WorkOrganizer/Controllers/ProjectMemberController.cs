using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Controllers
{
    [Authorize]
    public class ProjectMemberController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProjectMemberController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ProjectMember>>> Show(int id)
        {
            var users = await context.Users.ToListAsync();

            // TODO: filtrera bort ägaren
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var IdentityUserId = new Guid(userId);


            var members = await context.Member
                .Where(x => x.Project.Id == id)
                .Include(x => x.Project)
                .Include(x => x.Member)
                .ToListAsync();

            var userViewModelList = users.Select(user =>
            {
                return new UserViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    IsAdmin = members.Any(x => x.Member.Id == user.Id && x.IsAdmin),
                    IsMember = members.Any(x => x.Member.Id == user.Id)
                };
            });

            var viewModel = new ProjectMemberShowViewModel
            {
                ProjectId = id,
                Users = userViewModelList
            };

            return View(viewModel);
        }

        public IActionResult AddMember(int projectId, string userId)
        {
            var projectMember = new ProjectMember
            {
                ProjectId = projectId,
                MemberId = userId
            };

            context.Member.Add(projectMember);

            context.SaveChanges();

            return Ok();
        }


        public IActionResult RemoveMember(int projectId, string userId)
        {
            var projectMember = context.Member.FirstOrDefault(x => x.ProjectId == projectId && x.MemberId == userId);

            context.Member.Remove(projectMember);

            context.SaveChanges();

            return Ok();
        }

        public IActionResult AddAdmin(int projectId, string userId)
        {
            var projectMember = context.Member.FirstOrDefault(x => x.ProjectId == projectId && x.MemberId == userId);

            projectMember.IsAdmin = true;

            context.SaveChanges();

            return Ok();
        }


        public IActionResult RemoveAdmin(int projectId, string userId)
        {
            var projectMember = context.Member.FirstOrDefault(x => x.ProjectId == projectId && x.MemberId == userId);

            projectMember.IsAdmin = false;

            context.SaveChanges();

            return Ok();
        }


    }

    public class ProjectMemberShowViewModel
    {
        public int ProjectId { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsMember { get; set; }
        public bool IsAdmin { get; set; }
    }
}