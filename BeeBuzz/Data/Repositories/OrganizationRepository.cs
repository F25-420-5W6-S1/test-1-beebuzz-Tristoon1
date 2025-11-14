using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;

namespace BeeBuzz.Data.Repositories
{
    public class OrganizationRepository : BeeBuzzGenericGenericRepository<Organization>, IOrganizationRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<OrganizationRepository> _logger;

        public OrganizationRepository(ApplicationDbContext db, ILogger<OrganizationRepository> logger) : base(db, logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<ApplicationUser> GetAllUsersForOrganization(string organizationId)
        {
            return _db.Users.Where(user => user.Organization.OrganizationId == organizationId);
        }

        public IEnumerable<Beehive> GetAllBeehivesForOrganization(string organizationId)
        {
            return _db.Beehives
                .Where( beehive => 
                    beehive.User.Organization.OrganizationId.Equals(organizationId)
                );
        }
    }
}
