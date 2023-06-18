using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priority.Matrix.Manager.Presentation.Controllers
{
    [Route("api/category/{categoryId}/tasks")]
    public class TaskpriorityController : ControllerBase
    {
        private readonly IServiceManager _service;

        public TaskpriorityController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get Tasks per category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTaskPriorityForCategory(int categoryId)
        {
            var taskPriorityies = _service.TaskPriorityService.GetTaskPriorities(categoryId, tackChange: false);

            return Ok(taskPriorityies);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTaskPriorityForCategory(int categoryId, int id) 
        {
            var taskPriority = _service.TaskPriorityService.GetTaskPriority(categoryId, id, trackChange: false);

            return Ok(taskPriority);
        }
    }


}
