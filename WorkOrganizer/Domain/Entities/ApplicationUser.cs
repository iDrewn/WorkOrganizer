using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WorkOrganizer.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "User")]
        public string Name { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string SocialSecurityNumber { get; set; }

        [NotMapped]
        public bool IsSuperAdmin { get; set; }

    }
}
