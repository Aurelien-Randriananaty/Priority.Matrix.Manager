using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICategoryService
    {        
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);

        CategoryDto GetCategoryById(int categoryId, bool trackChanges);

        CategoryDto CreateCategory(CategoryForCreationDto categoryDto);

        void DeleteCategory(int categoryId, bool trackChanges);

        void UpdateCategory(int categoryId, CategoryForUpdateDto category, bool trackChanges);
    }
}
