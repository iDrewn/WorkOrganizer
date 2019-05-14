using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkOrganizer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Logging;

using WorkOrganizer.Domain.Repositories;
using WorkOrganizer.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WorkOrganizer.Areas.API.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using WorkOrganizer.Controllers;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          //  services.AddTransient();            //behövs detta? 

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //denna kod ner till rad 53 är ny, dvs vi har skrivit
             .AddJwtBearer(options =>
             {
                 var signingKey = Convert.FromBase64String(Configuration["jwt:SigningSecret"]);    //Stort C för configuration för det är en property

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(signingKey)

                 };
             });


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                //.AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            //Repositories
            services.AddScoped<IProjectRepository, ProjectRepository>();;

            //Services
            services.AddScoped<Domain.Services.IProjectService, Domain.Services.ProjectService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFileService, FileService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }




        // create user rolles
        //private async Task CreateUserRoles(IServiceProvider serviceProvider)
        //{
        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        //    string[] roleNames = { "Admin", "User" };
        //    IdentityResult roleResult;

        //    foreach (var roleName in roleNames)
        //    {
        //        var roleExist = await roleManager.RoleExistsAsync(roleName);

        //        if (!roleExist)
        //        {
        //            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        //        }
        //    }

        //    var poweruser = new IdentityUser
        //    {
        //        UserName = Configuration.GetSection("AppSettings")["UserEmail"],
        //        Email = Configuration.GetSection("AppSettings")["UserEmail"]
        //    };

        //    string userPassword = Configuration.GetSection("AppSettings")["UserPassword"];
        //    var user = await userManager.FindByEmailAsync(Configuration.GetSection("AppSettings")["UserPassword"]);

        //    if (user == null)
        //    {
        //        var createPowerUser = await userManager.CreateAsync(poweruser, userPassword);
        //        if (createPowerUser.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(poweruser, "Admin");
        //        }
        //    }
        //}




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            

            app.UseStaticFiles();

            
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //CreateUserRoles(serviceProvider).Wait();
        }
    }
}
