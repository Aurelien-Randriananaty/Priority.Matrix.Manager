using Microsoft.AspNetCore.Mvc;
using Priority.Matrix.Manager.Presentation.ActionFilters;
using Service.Contract;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public async Task<IActionResult> GetTaskPrioritiesForCategory(int categoryId, [FromQuery] TaskPriorityParameters taskPriorityParameters)
        {
            var pagesResult = await _service.TaskPriorityService.GetTaskPrioritiesAsync(categoryId, taskPriorityParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagesResult.metaData));

            return Ok(pagesResult.taskPriorities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTaskPriorityForCategory")]
        public async Task<IActionResult> GetTaskPriorityForCategory(int categoryId, int id)
        {
            var taskPriority = await _service.TaskPriorityService.GetTaskPriorityAsync(categoryId, id, trackChanges: false);

            return Ok(taskPriority);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateTaskPriorityForCategory(int categoryId, [FromBody] TaskPriorityForCreationDto taskPriority)
        {
            var taskPriorityToReturn = await _service.TaskPriorityService.CreateTaskPriorityForCategoryAsync(categoryId, taskPriority, trackChanges: false);

            return CreatedAtRoute("GetTaskPriorityForCategory", new
            {
                categoryId,
                id = taskPriorityToReturn.Id
            }, taskPriorityToReturn);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeteleTaskPriorityForCategory(int categoryId, int id)
        {
            await _service.TaskPriorityService.DeleteTaskPriorityForCategoryAsync(categoryId, id, trackChanges: true);
           
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateTaskPriorityForCategory(int categoryId, int id, [FromBody] TaskPriorityForUpdateDto taskPriority)
        {
            if (taskPriority is null)
                return BadRequest("EmployeeForUpdateDto object is null");

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.TaskPriorityService.UpdateTaskPriorityForCategoryAsync(categoryId, id, taskPriority, categoryTrackChanges: false, TaskPriorityTrackChanges: true);

            return NoContent();
        }
    }

}
