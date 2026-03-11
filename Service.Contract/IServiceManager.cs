namespace Service.Contract
{
    public interface IServiceManager
    {
        ICategoryService CategoryService { get; }

        ITaskPriorityService TaskPriorityService { get; }

        IAuthenticationService AuthenticationService { get; }

        IUserService UserService { get; }
    }
}
