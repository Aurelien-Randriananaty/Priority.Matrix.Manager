using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contract;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
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

        public async Task<(IEnumerable<TaskPriorityDto> taskPriorities, MetaData metaData)> GetTaskPrioritiesAsync(int categoryId, TaskPriorityParameters taskPriorityParameters,bool trackChanges)
        {
            if (!taskPriorityParameters.ValidHourRange)
                throw new MaxHourRangeBadRequestException();
            await CheckIfCategoryExists(categoryId, trackChanges);

            var taskPrioritiesWithMetaData = await _repository.TaskPriority.GetTaskPrioritiesAsync(categoryId, taskPriorityParameters,trackChanges);

            var taskPriorityDto = _mapper.Map<IEnumerable<TaskPriorityDto>>(taskPrioritiesWithMetaData);

            return (taskPriorities: taskPriorityDto, metaData: taskPrioritiesWithMetaData.MetaData);
        }

        public async Task<TaskPriorityDto> GetTaskPriorityAsync(int categoryId, int taskPriorityId, bool trackChanges)
        {
            await CheckIfCategoryExists(categoryId, trackChanges);

            var taskPriorityDb = await GetTaskPriorityForCategoryAndCheckIfExists(categoryId, taskPriorityId, trackChanges);

            var taskPriorityDto = _mapper.Map<TaskPriorityDto>(taskPriorityDb);

            return taskPriorityDto;
        }

        public async Task<(IEnumerable<TaskPriorityDto> taskPriorities, MetaData metaData)> GetTaskPrioritiesOnlyAsync(bool trackChanges, TaskPriorityParameters taskPriorityParameters)
        {
            if (!taskPriorityParameters.ValidHourRange)
                throw new MaxHourRangeBadRequestException();

            var taskPriorityWithMetaData = await _repository.TaskPriority.GetTaskPrioritiesOnlyAsync(trackChanges, taskPriorityParameters);
            
            var taskPriorityDto = _mapper.Map<IEnumerable<TaskPriorityDto>>(taskPriorityWithMetaData);
            return (taskPriorities: taskPriorityDto, metaData: taskPriorityWithMetaData.MetaData);
        }

        public async Task<TaskPriorityDto> CreateTaskPriorityForCategoryAsync(int categoryId, TaskPriorityForCreationDto taskPriorityForCreation, bool trackChanges)
        {
            await CheckIfCategoryExists(categoryId, trackChanges);

            var taskPriorityEntity = _mapper.Map<TaskPriority>(taskPriorityForCreation);

            _repository.TaskPriority.CreateTaskPriorityForCategory(categoryId, taskPriorityEntity);
            await _repository.SaveAsync();

            var emplyeeToReturn = _mapper.Map<TaskPriorityDto>(taskPriorityEntity);

            return emplyeeToReturn;
        }

        public async Task DeleteTaskPriorityForCategoryAsync(int CategoryId, int id, bool trackChanges)
        {
           await CheckIfCategoryExists(CategoryId, trackChanges);

            var taskPriorityForCategory = await GetTaskPriorityForCategoryAndCheckIfExists(CategoryId, id, trackChanges);

            _repository.TaskPriority.DeleteTaskPriority(taskPriorityForCategory);
            await _repository.SaveAsync();
        }

        public async Task UpdateTaskPriorityForCategoryAsync(int categoryId, int id, TaskPriorityForUpdateDto taskPriorityForUpdate, bool categoryTrackChanges, bool TaskPriorityTrackChanges)
        {
            await CheckIfCategoryExists(categoryId, categoryTrackChanges);

            var taskPriorityEntity = await GetTaskPriorityForCategoryAndCheckIfExists(categoryId, id, TaskPriorityTrackChanges);

            _mapper.Map(taskPriorityForUpdate, taskPriorityEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateTaskPriorityAsync(int categoryId, int id, TaskPriorityForUpdateDto taskPriorityForUpdate, bool categoryTrackChanges, bool TaskPriorityTrackChanges)
        {
            await CheckIfCategoryExists(categoryId, categoryTrackChanges);

            var taskPriorityEntity = await GetTaskPriorityAndCheckIfExists(id, TaskPriorityTrackChanges);

            _mapper.Map(taskPriorityForUpdate, taskPriorityEntity);
            await _repository.SaveAsync();
        }

        private async Task CheckIfCategoryExists(int categoryId, bool trackChanges)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);
        }

        private async Task<TaskPriority> GetTaskPriorityAndCheckIfExists(int id, bool TaskPriorityTrackChanges)
        {
            var taskPriorityEntity = await _repository.TaskPriority.GetTaskPriorityWithCategoryIdAsync(id, TaskPriorityTrackChanges);
            if (taskPriorityEntity is null)
                throw new TaskPriorityNotFoundException(id);

            return taskPriorityEntity;
        }

        private async Task<TaskPriority> GetTaskPriorityForCategoryAndCheckIfExists(int categoryId, int id, bool TaskPriorityTrackChanges)
        {
            var taskPriorityEntity = await _repository.TaskPriority.GetTaskPriorityAsync(categoryId, id, TaskPriorityTrackChanges);
            if (taskPriorityEntity is null)
                throw new TaskPriorityNotFoundException(categoryId);

            return taskPriorityEntity;
        }
        
    }
}
