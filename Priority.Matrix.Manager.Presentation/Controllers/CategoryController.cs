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
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get all Category
        /// </summary>
        /// <returns>retun all categories</returns>
        [HttpGet(Name = "Categories")]
        public IActionResult GetCategories()
        {
            var categories = _service.CategoryService.GetAllCategories(trackChange: false);

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "CategoryById")]
        public IActionResult GetCategory(int id)
        {
            var category = _service.CategoryService.GetCategoryById(id, trackChange: false);

            return Ok(category);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns>return category created</returns>
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryForCreationDto category)
        {
            if (category is null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCategory = _service.CategoryService.CreateCategory(category);

            return CreatedAtRoute("CategoryById", new {id = createdCategory.Id}, createdCategory);
        }
    }
}
