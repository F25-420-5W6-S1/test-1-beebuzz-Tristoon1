using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;

namespace BeeBuzz.Data.Repositories
{
    public class BeehiveRepository : BeeBuzzGenericGenericRepository<Beehive>, IBeehiveRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<BeehiveRepository> _logger;

        public BeehiveRepository(ApplicationDbContext db, ILogger<BeehiveRepository> logger) : base(db, logger)
        {
            _db = db;
            _logger = logger;
        }
    }
}
