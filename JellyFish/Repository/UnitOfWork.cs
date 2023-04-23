using JellyFish.Models;
using JellyFish.Repository.IRepository;

namespace JellyFish.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private JellyFishDbContext _context;

        public UnitOfWork(JellyFishDbContext context)
        {
            _context = context;
            Job = new JobRepository(_context);
            Category= new CategoryRepository(_context);
            JobType = new JobTypeRepository(_context);
            Level = new LevelRepository(_context);
        }

        public IJobRepository Job { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IJobTypeRepository JobType { get; private set; }
        public ILevelRepository Level { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
