using Domain.DTOs.CategoryDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.CategoryService;

public interface ICategoryService
{
    Task<Response<string>> AddCategoryAsync(AddCategoryDto addCategoryDto);
    Task<Response<bool>> DeleteCategoryAsync(int id);
    Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto categoryDto);
    Task<Response<GetCategoriesDto>> GetCategoryById(int id);
    Task<PagedResponse<List<GetCategoriesDto>>> GetCategoriesAsync(CategoryFilter filter);
}