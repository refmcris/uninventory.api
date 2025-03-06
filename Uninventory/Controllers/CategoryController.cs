using Microsoft.AspNetCore.Mvc;
using Uninventory.Interfaces;
using Uninventory.Models.Categories;

namespace Uninventory.Controllers
{
  [ApiController]
  [Route("api/")]
  public class CategoryController : ControllerBase
  {
    

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    [HttpPost("category")]
    public async Task<CategoryDTO> AddCategory(CategoryDTO add)
    {
      return await _categoryService.AddCategory(add);
    }

    [HttpGet("category")]
    public async Task<IEnumerable<CategoryDTO>> GetCategories(int? CategoryId)
    {
      return await _categoryService.GetCategories(CategoryId);
    }
  }
}
