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

        /// <summary>
        /// Get all Category
        /// </summary>
        /// <returns>retun all category</returns>
        [HttpGet(Name = "CetCompanies")]
        public IActionResult GetCategories()
        {
            var categories = _service.CategoryService.GetAllCategories(trackChange: false);

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategory(int id)
        {
            var category = _service.CategoryService.GetCategoryById(id, trackChange: false);

            return Ok(category);
        }
    }
}
