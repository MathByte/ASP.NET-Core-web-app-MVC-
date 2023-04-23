using JellyFish.Models;
using JellyFish.Models.View_Models;

namespace JellyFish.Repository.IRepository
{
    public interface IJobRepository : IRepository<Job>
    {
        void Add(Job job);
        void Update(Job obj);
        
    }
}
