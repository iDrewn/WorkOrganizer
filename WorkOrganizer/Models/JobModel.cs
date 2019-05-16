using System;
using WorkOrganizer.Domain.Entities;

namespace WorkOrganizer.Models
{
    public class JobModel
    {
        public Job JobLoader { get; set; }
        public Project ProjectLoader { get; set; }
    }
}