using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChange);

        Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids,bool trackChanges);

        Task<Category> GetCategoryAsync(int categoryId, bool trackChange);

        void CreateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
