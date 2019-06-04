namespace WorkOrganizer.Domain.Entities
{
    public class ProjectMember
    {
        public int Id { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public bool IsAdmin { get; set; }
        public ApplicationUser Member { get; set; }
        public string MemberId { get; set; }
        

    }
}
