using JellyFish.Repository.IRepository;
using JellyFish.Models;

namespace JellyFish.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private JellyFishDbContext _context;

        public JobRepository(JellyFishDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Job obj)
        {
            _context.Jobs.Update(obj);
        }
    }
}
