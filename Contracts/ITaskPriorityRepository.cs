using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITaskPriorityRepository
    {
        IEnumerable<TaskPriority> GetTaskPriorities(int categoryId, bool trackChange);

        TaskPriority GetTaskPriority (int categoryId, int taskPriorityId, bool tackChange);
    }
}
