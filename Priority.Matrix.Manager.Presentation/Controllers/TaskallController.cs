using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.RequestFeatures;
using System.Text.Json;

namespace Priority.Matrix.Manager.Presentation.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TaskallController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TaskallController(IServiceManager service)  => _service = service;

        [HttpGet(Name = "GetTaskPrioritiesOnly")]
        public async Task<IActionResult> GetTaskPrioritiesOnly([FromQuery] TaskPriorityParameters taskPriorityParameters)
        {
            var pagesResult = await _service.TaskPriorityService.GetTaskPrioritiesOnlyAsync(trackChanges: false, taskPriorityParameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagesResult.metaData));

            return Ok(pagesResult.taskPriorities);
        }
    }
}
