namespace WorkOrganizer.Models
{
    public class UserMemberViewModel 
    {
        
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsMember { get; set; }
        public bool IsAdmin { get; set; }

    }
}
