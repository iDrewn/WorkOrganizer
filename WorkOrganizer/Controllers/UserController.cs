using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkOrganizer.Data;

namespace WorkOrganizer.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Show()
        {
            return View(applicationDbContext.ApplicationUsers.ToList());
            
        }
    }
}