using AutoMapper;
using Contracts;
using Entities.Exceptions;
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

        public IEnumerable<TaskPriorityDto> GetTaskPriorities(int categoryId, bool trackChange)
        {
            var taskPriorities = _repository.TaskPriority.GetTaskPriorities(categoryId, trackChange);
            if (taskPriorities is null) 
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityFromDb = _repository.TaskPriority.GetTaskPriorities(categoryId, trackChange);

            var taskPriorityDto = _mapper.Map<IEnumerable<TaskPriorityDto>>(taskPriorities);

            return taskPriorityDto;
        }

        public TaskPriorityDto GetTaskPriority(int categoryId, int taskPriorityId, bool trackChange)
        {
            var category = _repository.Category.GetCategory(categoryId, trackChange);
            if (category is null)
                throw new CategoryNotFoundException(categoryId);

            var taskPriorityDb = _repository.TaskPriority.GetTaskPriority(categoryId, taskPriorityId, trackChange);
            if(taskPriorityDb is null)
                throw new TaskPriorityNotFoundException(taskPriorityId);

            var taskPriorityDto = _mapper.Map<TaskPriorityDto>(taskPriorityDb);

            return taskPriorityDto;
        }
    }
}
