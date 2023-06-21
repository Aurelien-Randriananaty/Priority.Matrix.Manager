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
    internal sealed class TaskPriorityService : ITaskPriorityService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TaskPriorityService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskPriorityDto>> GetTaskPrioritiesAsync(int categoryId, bool trackChanges)
        {
            var taskPriorities = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);
            if (taskPriorities is null) 
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityFromDb = await _repository.TaskPriority.GetTaskPrioritiesAsync(categoryId, trackChanges);

            var taskPriorityDto = _mapper.Map<IEnumerable<TaskPriorityDto>>(taskPriorityFromDb);

            return taskPriorityDto;
        }

        public async Task<TaskPriorityDto> GetTaskPriorityAsync(int categoryId, int taskPriorityId, bool trackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityDb = await _repository.TaskPriority.GetTaskPriorityAsync(categoryId, taskPriorityId, trackChanges);
            if(taskPriorityDb is null)
                throw new TaskPriorityNotFoundException(taskPriorityId);

            var taskPriorityDto = _mapper.Map<TaskPriorityDto>(taskPriorityDb);

            return taskPriorityDto;
        }

        public async Task<TaskPriorityDto> CreateTaskPriorityForCategoryAsync(int categoryId, TaskPriorityForCreationDto taskPriorityForCreation, bool trackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);
            if(category is null) 
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityEntity = _mapper.Map<TaskPriority>(taskPriorityForCreation);

            _repository.TaskPriority.CreateTaskPriorityForCategory(categoryId, taskPriorityEntity);
            await _repository.SaveAsync();

            var emplyeeToReturn = _mapper.Map<TaskPriorityDto>(taskPriorityEntity);

            return emplyeeToReturn;
        }

        public async Task DeleteTaskPriorityForCategoryAsync(int CategoryId, int id, bool trackChanges)
        {
            var categry = await _repository.Category.GetCategoryAsync(CategoryId, trackChanges);
            if(categry is null)
                throw new CategoryNotFoundException(CategoryId);

            var taskPriorityForCategory = await _repository.TaskPriority.GetTaskPriorityAsync(CategoryId, id, trackChanges);
            if(taskPriorityForCategory is null)
                throw new TaskPriorityNotFoundException(CategoryId);

            _repository.TaskPriority.DeleteTaskPriority(taskPriorityForCategory);
            await _repository.SaveAsync();
        }

        public async Task UpdateTaskPriorityForCategoryAsync(int categoryId, int id, TaskPriorityForUpdateDto taskPriorityForUpdate, bool categoryTrackChanges, bool TaskPriorityTrackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, categoryTrackChanges);
            if(category is null)
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityEntity = await _repository.TaskPriority.GetTaskPriorityAsync(categoryId, id, TaskPriorityTrackChanges);
            if(taskPriorityEntity is null)
                throw new TaskPriorityNotFoundException(categoryId);

            _mapper.Map(taskPriorityForUpdate, taskPriorityEntity);
            await _repository.SaveAsync();
        }
    }
}
