using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contract;
using Shared.DataTransferObjects;
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
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public CategoryDto  CreateCategory(CategoryForCreationDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            _repository.Category.CreateCategory(categoryEntity);
            _repository.Save();

            var companyToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return companyToReturn;
        }

        public IEnumerable<CategoryDto> GetAllCategories(bool trackChange)
        {
            var categories = _repository.Category.GetAllCategories(trackChange);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoriesDto;
        }

        public CategoryDto GetCategoryById(int categoryId, bool trackChange)
        {
            var category = _repository.Category.GetCategory(categoryId, trackChange);

            if (category is null)
                throw new CategoryNotFoundException(categoryId);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}
