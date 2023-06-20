using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects;
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
            var taskPriorityies = _service.TaskPriorityService.GetTaskPriorities(categoryId, trackChanges: false);

            return Ok(taskPriorityies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTaskPriorityForCategory")]
        public IActionResult GetTaskPriorityForCategory(int categoryId, int id)
        {
            var taskPriority = _service.TaskPriorityService.GetTaskPriority(categoryId, id, trackChanges: false);

            return Ok(taskPriority);
        }

        [HttpPost]
        public IActionResult CreateTaskPriorityForCategory(int categoryId, [FromBody] TaskPriorityForCreationDto taskPriority)
        {
            if (taskPriority is null)
                return BadRequest("TaskPriorityForCreationDto ogject is nulle");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var taskPriorityToReturn = _service.TaskPriorityService.CreateTaskPriorityForCategory(categoryId, taskPriority, trackChanges: false);

            return CreatedAtRoute("GetTaskPriorityForCategory", new
            {
                categoryId,
                id = taskPriorityToReturn.Id
            }, taskPriorityToReturn);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeteleTaskPriorityForCategory(int categoryId, int id)
        {
            _service.TaskPriorityService.DeleteTaskPriorityForCategory(categoryId, id, trackChanges: true);
           
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTaskPriorityForCategory(int categoryId, int id, [FromBody] TaskPriorityForUpdateDto taskPriority)
        {
            if (taskPriority is null)
                return BadRequest("EmployeeForUpdateDto object is null");

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            _service.TaskPriorityService.UpdateTaskPriorityForCategory(categoryId, id, taskPriority, categoryTrackChanges: false, TaskPriorityTrackChanges: true);

            return NoContent();
        }
    }

}
