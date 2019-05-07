using System;

namespace WorkOrganizer.Domain.Entities
{
    public class Job
    {
        public Job(string name, string description, string material, DateTime date, string hours, string projectId)
        {
            Name = name;
            Description = description;
            Material = material;
            Date = date;
            Hours = hours;
            ProjectId = projectId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
        public string ProjectId { get; set; }
        public Project Project { get; set; }
    }
}