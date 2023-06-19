using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICategoryService
    {        
        IEnumerable<CategoryDto> GetAllCategories(bool trackChange);

        CategoryDto GetCategoryById(int categoryId, bool trackChange);

        CategoryDto CreateCategory(CategoryForCreationDto categoryDto);
    }
}
