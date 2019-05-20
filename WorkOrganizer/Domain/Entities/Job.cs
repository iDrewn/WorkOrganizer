using System;
using WorkOrganizer.Models;

namespace WorkOrganizer.Domain.Entities
{
    public class Job
    {
        public Job(string name, string description, string material, DateTime date, string hours, bool isDone)
        {
            Name = name;
            Description = description;
            Material = material;
            Date = date;
            Hours = hours;
            IsDone = isDone;
        }
        public Job(){}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
        public bool IsDone { get; set; }
        public int ProjectId { get; set; }
    }
}