using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<TaskPriority>> GetTaskPrioritiesAsync(int categoryId, bool trackChanges) =>
            await FindByCondition(e => e.CategoryID.Equals(categoryId), trackChanges)
            .OrderBy(e => e.Id)
            .ToListAsync();

        public async Task<TaskPriority> GetTaskPriorityAsync(int categoryId, int taskPriorityId, bool trackChanges) =>
            await FindByCondition(t => t.CategoryID.Equals(categoryId) && t.Id.Equals(taskPriorityId), trackChanges)
            .FirstOrDefaultAsync();

        public void CreateTaskPriorityForCategory(int categoryId, TaskPriority taskPriority)
        {
            taskPriority.CategoryID = categoryId;
            Create(taskPriority);
        }

        public void DeleteTaskPriority(TaskPriority taskPriority) => Delete(taskPriority);
    }
}
