using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Areas.API.Models;
using WorkOrganizer.Areas.API.Services;
using WorkOrganizer.Domain.Services;

namespace WorkOrganizer.Areas.API.Controllers                    
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;

        public UsersController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync(UserDto userDto)
        {
            var user = await applicationUserService.CreateUserAsync(userDto.Email, userDto.Password, userDto.Name, userDto.Firstname, userDto.Lastname, userDto.SocialSecurityNumber);

            return Ok(user);
        }
    }
}