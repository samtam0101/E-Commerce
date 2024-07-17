using Domain.Constants;
using Domain.DTOs.CategoryDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Permissions;
using Infrastructure.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost("Add-Category")]
    [PermissionAuthorize(Permissions.Categories.Create)]
    public async Task<Response<string>> AddCategoryAsync([FromBody] AddCategoryDto addCategoryDto)
    {
        return await categoryService.AddCategoryAsync(addCategoryDto);
    }
    [HttpGet("Get-Categories")]
    [PermissionAuthorize(Permissions.Categories.View)]
    public async Task<PagedResponse<List<GetCategoriesDto>>> GetCategorysAsync([FromQuery] CategoryFilter filter)
    {
        return await categoryService.GetCategoriesAsync(filter);
    }
    [HttpGet("Get-Category-By-Id")]
    [PermissionAuthorize(Permissions.Categories.View)]
    public async Task<Response<GetCategoriesDto>> GetCategoryByIdAsync([FromQuery] int id)
    {
        return await categoryService.GetCategoryById(id);
    }
    [HttpPut("Update-Category")]
    [PermissionAuthorize(Permissions.Categories.Edit)]
    public async Task<Response<string>> UpdateCategoryAsync([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        return await categoryService.UpdateCategoryAsync(updateCategoryDto);
    }
    [HttpDelete("Delete-Category")]
    [PermissionAuthorize(Permissions.Categories.Delete)]
    public async Task<Response<bool>> DeleteCategoryAsync(int id)
    {
        return await categoryService.DeleteCategoryAsync(id);
    }
}