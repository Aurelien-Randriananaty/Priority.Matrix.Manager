using AutoMapper;
using Contracts;
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

        public IEnumerable<CategoryDto> GetAllCategories(bool trackChange)
        {
            try
            {
                var categories = _repository.Category.GetAllCategories(trackChange);
               
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
               
                return categoriesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the { nameof(GetAllCategories)} service method { ex} ");
                throw;
            }
        }
    }
}
