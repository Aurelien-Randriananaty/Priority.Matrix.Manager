namespace Contracts
{
    public interface IRepositoryManager
    {
        ITaskPriorityRepository TaskPriority { get; }

        ICategoryRepository Category { get; }

        Task SaveAsync();
    }
}
