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
        Task<IEnumerable<TaskPriorityDto>> GetTaskPrioritiesAsync(int categoryId, bool trackChanges);

        Task<TaskPriorityDto> GetTaskPriorityAsync(int categoryId, int taskPriorityId, bool trackChanges);

        Task<TaskPriorityDto> CreateTaskPriorityForCategoryAsync(int categoryId, TaskPriorityForCreationDto taskPriorityForCreation, bool trackChanges);

        Task DeleteTaskPriorityForCategoryAsync(int CategoryId, int id, bool trackChanges);

        Task UpdateTaskPriorityForCategoryAsync(int categoryId, int id, TaskPriorityForUpdateDto taskPriorityForUpdate, bool categoryTrackChanges, bool TaskPriorityTrackChanges);
    }
}
