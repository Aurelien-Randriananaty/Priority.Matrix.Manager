using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects;

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
            var categories = _service.CategoryService.GetAllCategories(trackChanges: false);

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "CategoryById")]
        public IActionResult GetCategory(int id)
        {
            var category = _service.CategoryService.GetCategoryById(id, trackChanges: false);

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
            if (category == null)
                return BadRequest("CompanyForCreationDto object is null");

            var createdCategory = _service.CategoryService.CreateCategory(category);

            return CreatedAtRoute("CategoryById", new {id = createdCategory.Id}, createdCategory);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            _service.CategoryService.DeleteCategory(id, trackChanges: false);

            return NoContent();
        }
    }
}
