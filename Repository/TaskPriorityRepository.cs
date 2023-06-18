using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TaskPriorityRepository : RepositoryBase<TaskPriority>, ITaskPriorityRepository
    {
        public TaskPriorityRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<TaskPriority> GetTaskPriorities(int categoryId, bool trackChange) =>
            FindByCondition(e => e.CategoryID.Equals(categoryId), trackChange)
            .OrderBy(e => e.Id)
            .ToList();

        public TaskPriority GetTaskPriority(int categoryId, int taskPriorityId, bool tackChange) =>
            FindByCondition(t => t.CategoryID.Equals(categoryId) && t.Id.Equals(taskPriorityId), tackChange)
            .FirstOrDefault();
    }
}
