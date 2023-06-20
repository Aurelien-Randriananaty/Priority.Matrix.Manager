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

        public void CreateTaskPriorityForCategory(int categoryId, TaskPriority taskPriority)
        {
            taskPriority.CategoryID = categoryId;
            Create(taskPriority);
        }

        public IEnumerable<TaskPriority> GetTaskPriorities(int categoryId, bool trackChanges) =>
            FindByCondition(e => e.CategoryID.Equals(categoryId), trackChanges)
            .OrderBy(e => e.Id)
            .ToList();

        public TaskPriority GetTaskPriority(int categoryId, int taskPriorityId, bool trackChanges) =>
            FindByCondition(t => t.CategoryID.Equals(categoryId) && t.Id.Equals(taskPriorityId), trackChanges)
            .FirstOrDefault();
    }
}
