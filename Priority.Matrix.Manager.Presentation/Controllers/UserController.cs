using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;

namespace Priority.Matrix.Manager.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserController(IServiceManager service )
        {
           _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers() {
            var users = await _service.UserService.GetUsersAsync();

            return Ok( users );
        }
    }
}
