using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public void CreateCategory(Category category) => Create(category);

        public IEnumerable<Category> GetAllCategories(bool trackChange) => FindAll(trackChange)
            .OrderBy(c => c.Id)
            .ToList();

        public Category GetCategory(int categoryId, bool trackChange) => 
            FindByCondition(c => c.Id.Equals(categoryId), trackChange)
            .SingleOrDefault();
    }
}
