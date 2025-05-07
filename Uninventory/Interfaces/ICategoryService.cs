using Uninventory.Models.Categories;

namespace Uninventory.Interfaces
{
  public interface ICategoryService
  {
    public Task<CategoryDTO> AddCategory(CategoryDTO add);
    public Task<IEnumerable<CategoryDTO>> GetCategories(int? CategoryId);
  }
}
