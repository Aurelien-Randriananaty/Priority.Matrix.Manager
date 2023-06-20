using Shared.DataTransferObjects;

namespace Service.Contract
{
    public interface ICategoryService
    {        
        IEnumerable<CategoryDto> GetAllCategories(bool trackChanges);

        IEnumerable<CategoryDto> GetByIds(IEnumerable<int> ids, bool trackChanges);

        CategoryDto GetCategoryById(int categoryId, bool trackChanges);

        CategoryDto CreateCategory(CategoryForCreationDto categoryDto);

        void DeleteCategory(int categoryId, bool trackChanges);

        void UpdateCategory(int categoryId, CategoryForUpdateDto category, bool trackChanges);
    }
}
