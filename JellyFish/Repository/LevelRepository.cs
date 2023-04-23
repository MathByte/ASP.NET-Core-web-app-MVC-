using JellyFish.Models;
using JellyFish.Repository.IRepository;

namespace JellyFish.Repository
{
    public class LevelRepository : Repository<Level>, ILevelRepository
    {
        private JellyFishDbContext _context;

        public LevelRepository(JellyFishDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Level obj)
        {
            _context.Levels.Update(obj);
        }
    }
}
