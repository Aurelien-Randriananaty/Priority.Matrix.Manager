using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICategoryRepository> _categoryRepository; 
    private readonly Lazy<ITaskPriorityRepository> _taskPriorityRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
        _taskPriorityRepository = new Lazy<ITaskPriorityRepository>(() => new TaskPriorityRepository(repositoryContext));

    }

    public ITaskPriorityRepository TaskPriority => _taskPriorityRepository.Value;

    public ICategoryRepository Category => _categoryRepository.Value;

    public void Save() => _repositoryContext.SaveChanges();
}
