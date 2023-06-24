using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class TaskPriorityNotFoundException : NotFoundException
    {
        public TaskPriorityNotFoundException(int taskPriorityId) : 
            base($"Task with id: {taskPriorityId} doesn't exist in the database.")
        {
        }
    }
}
