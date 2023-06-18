using Contracts;
using Entities.Models;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CategoryService : ICategoryService
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CategoryService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Category> GetAllCategories(bool trackChange)
        {
            try
            {
                var categories = _repository.Category.GetAllCategories(trackChange);
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the { nameof(GetAllCategories)} service method { ex} ");
                throw;
            }
        }
    }
}
