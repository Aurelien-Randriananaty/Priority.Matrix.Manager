using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Priority.Matrix.Manager.Presentation.ActionFilters;
using Service.Contract;
using Shared.DataTransferObjects;

namespace Priority.Matrix.Manager.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CategoryController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get all Category
        /// </summary>
        /// <returns>retun all categories</returns>
        [HttpGet(Name = "Categories")]
        //[Authorize]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _service.CategoryService.GetAllCategoriesAsync(trackChanges: false);

            return Ok(categories);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _service.CategoryService.GetCategoryByIdAsync(id, trackChanges: false);

            return Ok(category);
        }

        [HttpGet("CollectionExtensions/{ids}", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCategoryCollection(IEnumerable<int> ids)
        {
            var categories = await _service.CategoryService.GetByIdsAsync(ids, trackChanges: false);

            return Ok(categories);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns>return category created</returns>
        [HttpPost(Name = "CreateCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreationDto category)
        {
            var createdCategory = await _service.CategoryService.CreateCategoryAsync(category);

            return CreatedAtRoute("CategoryById", new {id = createdCategory.Id}, createdCategory);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _service.CategoryService.DeleteCategoryAsync(id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "EditCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryForUpdateDto category)
        {
            await _service.CategoryService.UpdateCategoryAsync(id, category, trackChanges: true);

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetCategoriesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
