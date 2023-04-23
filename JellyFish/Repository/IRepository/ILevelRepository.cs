using JellyFish.Models;

namespace JellyFish.Repository.IRepository
{
    public interface ILevelRepository : IRepository<Level>
    {
        void Update(Level obj);
    }
}
