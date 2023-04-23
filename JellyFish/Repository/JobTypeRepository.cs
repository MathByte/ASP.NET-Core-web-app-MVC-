using JellyFish.Models;
using JellyFish.Repository.IRepository;

namespace JellyFish.Repository
{
    public class JobTypeRepository : Repository<JobType>, IJobTypeRepository
    {
        private JellyFishDbContext _context;

        public JobTypeRepository(JellyFishDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(JobType obj)
        {
            _context.JobTypes.Update(obj);
        }


    }
}
