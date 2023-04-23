using JellyFish.Models;

namespace JellyFish.Repository.IRepository
{
    public interface IJobTypeRepository : IRepository<JobType>
    {
        void Update(JobType obj);
    }
}
