﻿using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITaskPriorityRepository
    {
        Task<PagedList<TaskPriority>> GetTaskPrioritiesAsync(int categoryId, TaskPriorityParameters taskPriorityParameters, bool trackChanges);

        Task<TaskPriority> GetTaskPriorityAsync(int categoryId, int taskPriorityId, bool tackChanges);

        void CreateTaskPriorityForCategory(int  categoryId, TaskPriority taskPriority);

        void DeleteTaskPriority(TaskPriority taskPriority);
    }
}
