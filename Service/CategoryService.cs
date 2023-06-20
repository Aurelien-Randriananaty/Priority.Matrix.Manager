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

        public void DeleteCategory(int categoryId, bool trackChanges)
        {
            var category = _repository.Category.GetCategory(categoryId, trackChanges);
            if (category == null) 
                throw new CategoryNotFoundException(categoryId);

            _repository.Category.DeleteCategory(category);
            _repository.Save();
        }

        public IEnumerable<CategoryDto> GetAllCategories(bool trackChanges)
        {
            var categories = _repository.Category.GetAllCategories(trackChanges);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoriesDto;
        }

        public CategoryDto GetCategoryById(int categoryId, bool trackChanges)
        {
            var category = _repository.Category.GetCategory(categoryId, trackChanges);

            if (category is null)
                throw new CategoryNotFoundException(categoryId);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public void UpdateCategory(int categoryId, CategoryForUpdateDto categoryForUpdateDto, bool trackChanges)
        {
            var categoryEntity = _repository.Category.GetCategory(categoryId, trackChanges);
            if (categoryEntity is null)
                throw new CategoryNotFoundException(categoryId);

            _mapper.Map(categoryForUpdateDto, categoryEntity);
            _repository.Save();
        }
    }
}
