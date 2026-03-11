using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contract;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ITaskPriorityService> _taskPriorityService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IUserService> _userService;

        public ServiceManager(
            IRepositoryManager repositoryManager, 
            ILoggerManager logger, IMapper mapper, 
            UserManager<User> userManager,
            IOptions<JwtConfiguration> configuration)
        {
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(repositoryManager, logger, mapper));
            _taskPriorityService = new Lazy<ITaskPriorityService>(() => new TaskPriorityService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
            _userService = new Lazy<IUserService>(() => new UserService(logger, mapper, userManager));
        }

        public ICategoryService CategoryService => _categoryService.Value;

        public ITaskPriorityService TaskPriorityService => _taskPriorityService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IUserService UserService => _userService.Value;
    }
}
