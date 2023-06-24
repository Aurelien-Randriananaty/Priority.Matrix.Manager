using AutoMapper;
using Contracts;
using Service.Contract;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ITaskPriorityService> _taskPriorityService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, logger, mapper));
            _taskPriorityService = new Lazy<ITaskPriorityService>(() => new TaskPriorityService(repositoryManager, logger, mapper));
        }

        public ICategoryService CategoryService => _categoryService.Value;

        public ITaskPriorityService TaskPriorityService => _taskPriorityService.Value;
    }
}
