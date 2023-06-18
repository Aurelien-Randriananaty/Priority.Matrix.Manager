using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priority.Matrix.Manager.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service) => _service = service;

        public IActionResult GetCategories()
        {
            try
            {
                var categories = _service.CategoryService.GetAllCategories(trackChange: false);

                return Ok(categories);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
