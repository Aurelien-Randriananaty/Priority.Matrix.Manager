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
        IEnumerable<TaskPriorityDto> GetTaskPriorities(int categoryId, bool tackChange);

        TaskPriorityDto GetTaskPriority(int categoryId, int taskPriorityId, bool trackChange);
    }
}
