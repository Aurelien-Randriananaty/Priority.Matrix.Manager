using Contracts;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TaskPriorityService : ITaskPriorityService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public TaskPriorityService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
