﻿using System;

namespace WorkOrganizer.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }

        // work IList
    }
}
