using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkOrganizer.Domain.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public int ProjectId { get; set; }

    }
}
