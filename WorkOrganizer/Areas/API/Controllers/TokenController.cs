using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Areas.API.Models;
using WorkOrganizer.Areas.API.Services;

namespace WorkOrganizer.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public TokenController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)                      //[FromBody] string username, string password
        {
            var token = await tokenService.GetTokenAsync(loginDto.Username, loginDto.Password);

            if (token == null) return Unauthorized();

            return Ok(token);

            //return token == null            // tidigare Ok();
            //    ? Unauthorized()        //401 Unauthorized
            //    : Ok(new { token });
        }
    }
}
