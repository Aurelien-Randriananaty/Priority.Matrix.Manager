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

        public IEnumerable<TaskPriorityDto> GetTaskPriorities(int categoryId, bool trackChanges)
        {
            var taskPriorities = _repository.TaskPriority.GetTaskPriorities(categoryId, trackChanges);
            if (taskPriorities is null || taskPriorities.Count() == 0) 
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityFromDb = _repository.TaskPriority.GetTaskPriorities(categoryId, trackChanges);

            var taskPriorityDto = _mapper.Map<IEnumerable<TaskPriorityDto>>(taskPriorities);

            return taskPriorityDto;
        }

        public TaskPriorityDto GetTaskPriority(int categoryId, int taskPriorityId, bool trackChanges)
        {
            var category = _repository.Category.GetCategory(categoryId, trackChanges);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityDb = _repository.TaskPriority.GetTaskPriority(categoryId, taskPriorityId, trackChanges);
            if(taskPriorityDb is null)
                throw new TaskPriorityNotFoundException(taskPriorityId);

            var taskPriorityDto = _mapper.Map<TaskPriorityDto>(taskPriorityDb);

            return taskPriorityDto;
        }

        public TaskPriorityDto CreateTaskPriorityForCategory(int categoryId, TaskPriorityForCreationDto taskPriorityForCreation, bool trackChanges)
        {
            var category = _repository.Category.GetCategory(categoryId, trackChanges);
            if(category is null) 
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityEntity = _mapper.Map<TaskPriority>(taskPriorityForCreation);

            _repository.TaskPriority.CreateTaskPriorityForCategory(categoryId, taskPriorityEntity);
            _repository.Save();

            var emplyeeToReturn = _mapper.Map<TaskPriorityDto>(taskPriorityEntity);

            return emplyeeToReturn;
        }

        public void DeleteTaskPriorityForCategory(int CategoryId, int id, bool trackChanges)
        {
            var categry = _repository.Category.GetCategory(CategoryId, trackChanges);
            if(categry is null)
                throw new CategoryNotFoundException(CategoryId);

            var taskPriorityForCategory = _repository.TaskPriority.GetTaskPriority(CategoryId, id, trackChanges);
            if(taskPriorityForCategory is null)
                throw new TaskPriorityNotFoundException(CategoryId);

            _repository.TaskPriority.DeleteTaskPriority(taskPriorityForCategory);
            _repository.Save();
        }

        public void UpdateTaskPriorityForCategory(int categoryId, int id, TaskPriorityForUpdateDto taskPriorityForUpdate, bool categoryTrackChanges, bool TaskPriorityTrackChanges)
        {
            var category = _repository.Category.GetCategory(categoryId, categoryTrackChanges);
            if(category is null)
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityEntity = _repository.TaskPriority.GetTaskPriority(categoryId, id, TaskPriorityTrackChanges);
            if(taskPriorityEntity is null)
                throw new TaskPriorityNotFoundException(categoryId);

            _mapper.Map(taskPriorityForUpdate, taskPriorityEntity);
            _repository.Save();
        }
    }
}
