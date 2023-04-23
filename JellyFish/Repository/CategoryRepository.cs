using JellyFish.Repository.IRepository;
using JellyFish.Models;

namespace JellyFish.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private JellyFishDbContext _context;

        public CategoryRepository(JellyFishDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }
    }
}
