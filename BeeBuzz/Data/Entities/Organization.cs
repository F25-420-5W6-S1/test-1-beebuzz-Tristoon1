namespace BeeBuzz.Data.Entities
{
    public class Organization
    {
        public string OrganizationId { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
