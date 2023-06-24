using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICategoryService
    {        
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges);

        Task<IEnumerable<CategoryDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);

        Task<CategoryDto> GetCategoryByIdAsync(int categoryId, bool trackChanges);

        Task<CategoryDto> CreateCategoryAsync(CategoryForCreationDto categoryDto);

        Task DeleteCategoryAsync(int categoryId, bool trackChanges);

        Task UpdateCategoryAsync(int categoryId, CategoryForUpdateDto category, bool trackChanges);
    }
}
