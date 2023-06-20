using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract
{
    public interface ITaskPriorityService
    {
        IEnumerable<TaskPriorityDto> GetTaskPriorities(int categoryId, bool trackChanges);

        TaskPriorityDto GetTaskPriority(int categoryId, int taskPriorityId, bool trackChanges);

        TaskPriorityDto CreateTaskPriorityForCategory(int categoryId, TaskPriorityForCreationDto taskPriorityForCreation, bool trackChanges);

        void DeleteTaskPriorityForCategory(int CategoryId,  int id, bool trackChanges);
    }
}
