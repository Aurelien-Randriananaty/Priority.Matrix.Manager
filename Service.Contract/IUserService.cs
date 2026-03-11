using Shared.DataTransferObjects;

namespace Service.Contract;

public interface IUserService
{
    Task<IEnumerable<UserIdentitiesDto>> GetUsersAsync();
}
