using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
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

        public async Task<PagedList<TaskPriority>> GetTaskPrioritiesAsync(int categoryId, TaskPriorityParameters taskPriorityParameters,bool trackChanges)
        {
           var taskPriorities = await FindByCondition(e => e.CategoryID.Equals(categoryId), trackChanges)
            .OrderBy(e => e.Id)
            .ToListAsync();

            return PagedList<TaskPriority>.ToPagedList(
                taskPriorities, 
                taskPriorityParameters.PageNumber, 
                taskPriorityParameters.PageSize);

        }

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
