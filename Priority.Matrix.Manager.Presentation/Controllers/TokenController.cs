using Microsoft.AspNetCore.Mvc;
using Priority.Matrix.Manager.Presentation.ActionFilters;
using Service.Contract;
using Shared.DataTransferObjects;

namespace Priority.Matrix.Manager.Presentation.Controllers
{
    [Route("api/token")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _service;
        public TokenController(IServiceManager service) => _service = service;

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _service.AuthenticationService.RefreshToken(tokenDto);

            return Ok(tokenDtoToReturn);
        }
    }
}
