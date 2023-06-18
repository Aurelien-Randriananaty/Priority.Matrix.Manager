using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICategoryService
    {        
        IEnumerable<CategoryDto> GetAllCategories(bool trackChange);
    }
}
