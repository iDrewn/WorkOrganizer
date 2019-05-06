using Microsoft.AspNetCore.Identity;

namespace WorkOrganizer.Domain.Entities
{
    public class User : IdentityUser
    {   
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string SocialSecurityNumber { get; set; }
    }
}
