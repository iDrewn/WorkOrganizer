using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WorkOrganizer.Domain.Entities
{
    public class Project
    {
        public Project(string name, DateTime startDate, DateTime endDate, string description, string identityUserId)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            IdentityUserId = identityUserId;
        }

        public Project()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string IdentityUserId {get; set;} 
        public IdentityUser User { get; set; }
        public IList<Job> Jobs { get; set; }

    }
}
