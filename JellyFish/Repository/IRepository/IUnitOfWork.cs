namespace JellyFish.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IJobRepository Job { get; }

        ICategoryRepository Category { get; }

        IJobTypeRepository JobType { get; }

        ILevelRepository Level { get; }

        void Save();
    }
}
