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

        public async Task<CategoryDto>  CreateCategoryAsync(CategoryForCreationDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _repository.Category.CreateCategory(categoryEntity);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return companyToReturn;
        }

        public async Task DeleteCategoryAsync(int categoryId, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfItExists(categoryId, trackChanges);

            _repository.Category.DeleteCategory(category);
           await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
        {
            var categories = await _repository.Category.GetAllCategoriesAsync(trackChanges);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoriesDto;
        }

        public async Task<IEnumerable<CategoryDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
        {
            if(ids is null)
                throw new IdParametersBadRequestException();

            var categoryEntities = await _repository.Category.GetByIdsAsync(ids, trackChanges);
            if(ids.Count() != categoryEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var categoryToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);

            return categoryToReturn;
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId, bool trackChanges)
        {
            var category = await GetCategoryAndCheckIfItExists(categoryId, trackChanges);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

        public async Task UpdateCategoryAsync(int categoryId, CategoryForUpdateDto categoryForUpdateDto, bool trackChanges)
        {
            var categoryEntity = await GetCategoryAndCheckIfItExists(categoryId, trackChanges);

            _mapper.Map(categoryForUpdateDto, categoryEntity);
            await _repository.SaveAsync();
        }

        private async Task<Category> GetCategoryAndCheckIfItExists(int id, bool trackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(id, trackChanges);

            if (category is null)
                throw new CategoryNotFoundException(id);

            return category;
        }
    }
}
