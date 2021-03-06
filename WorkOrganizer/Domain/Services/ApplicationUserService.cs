﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkOrganizer.Data;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Domain.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;         //behövs dessa två rader?
        //private readonly ILogger<RegisterModel> logger;

       private readonly ApplicationDbContext context;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            //this.logger = logger;
            this.context = context;
        }
        

        public async Task<ApplicationUser> CreateUserAsync(string email, string password, string name, string firstName, string lastName, string socialSecurityNumber)
        {

            var applicationUser = new ApplicationUser
            {
                // UserName = Input.Email,          //behövs?
                Email = email,
                Name = name,
                Firstname = firstName,
                Lastname = lastName,
                SocialSecurityNumber = socialSecurityNumber
            };

            var result = await userManager.CreateAsync(applicationUser, password);

            //if (result.Succeeded)
            //{
            //if (!await roleManager.RoleExistsAsync(SD.AdminEndUser))
            //{
            //    await roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser));
            //}
            //if (!await roleManager.RoleExistsAsync(SD.SuperAdminEndUser))
            //{
            //    await roleManager.CreateAsync(new IdentityRole(SD.SuperAdminEndUser));
            //}

            //if (Input.IsSuperAdmin)                       //ska detta vara med?
            //{
            //    await _userManager.AddToRoleAsync(user, SD.SuperAdminEndUser);
            //}
            //else
            //{
            //    await _userManager.AddToRoleAsync(user, SD.AdminEndUser);
            //}

            //_logger.LogInformation("User created a new account with password.");

            //var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

            //var callbackUrl = Url.Page(
            //    "/Account/ConfirmEmail",
            //    pageHandler: null,
            //    values: new { userId = user.Id, code = code },
            //    protocol: Request.Scheme);

            //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            //await _signInManager.SignInAsync(user, isPersistent: false);
            //return LocalRedirect(returnUrl);
            //}

            context.Users.Add(applicationUser);
            await context.SaveChangesAsync();

            return applicationUser;
        }
        public async Task<IEnumerable<ApplicationUser>> SearchUser(string searchString)
        {
            var searchJobs = context.ApplicationUsers.Where(s => s.UserName.Contains(searchString));
            return searchJobs;
        }
    }
}
