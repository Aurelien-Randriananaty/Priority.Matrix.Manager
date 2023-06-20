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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "CategoryById")]
        public IActionResult GetCategory(int id)
        {
            var category = _service.CategoryService.GetCategoryById(id, trackChanges: false);

            return Ok(category);
        }

        [HttpGet("CollectionExtensions/{ids}", Name = "CompanyCollection")]
        public IActionResult GetCategoryCollection(IEnumerable<int> ids)
        {
            var categories = _service.CategoryService.GetByIds(ids, trackChanges: false);

            return Ok(categories);
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
                return BadRequest("CategoryForCreationDto object is null");

            var createdCategory = _service.CategoryService.CreateCategory(category);

            return CreatedAtRoute("CategoryById", new {id = createdCategory.Id}, createdCategory);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            _service.CategoryService.DeleteCategory(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryForUpdateDto category)
        {
            if (category is null)
                return BadRequest("CategoryForUpdateDto object is null");

            _service.CategoryService.UpdateCategory(id, category, trackChanges: true);

            return NoContent();
        }
    }
}
