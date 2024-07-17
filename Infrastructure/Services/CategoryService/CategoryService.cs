using System.Net;
using Domain.DTOs.CategoryDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.CategoryService;

public class CategoryService(DataContext context, ILogger<CategoryService> logger) : ICategoryService
{
    public async Task<Response<string>> AddCategoryAsync(AddCategoryDto addCategoryDto)
    {
        try
        {
            logger.LogInformation("AddCategory method started at {DateTime}", DateTime.UtcNow);
            var existingCategory = await context.Categories.AnyAsync(x => x.Name == addCategoryDto.Name);
            if (existingCategory)
            {
                logger.LogWarning("Category with name {CategoryName} already exists, Time: {DateTime}", addCategoryDto.Name, DateTime.UtcNow);
                return new Response<string>(HttpStatusCode.BadRequest, "Category with this name already exists");
            }
            var category = new Category()
            {
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            logger.LogInformation("AddCategory method ended at {DateTime}", DateTime.UtcNow);
            return new Response<string>($"Category created successfully, It's id: {category.CategoryId}");
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCategoryAsync(int id)
    {
        try
        {
            logger.LogInformation("DeleteCategory method started at {DateTime}", DateTime.UtcNow);
            var existing = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (existing == null)
            {
                logger.LogWarning("Category with id {CategoryId} not found, Time: {DateTime}", id, DateTime.UtcNow);
                return new Response<bool>(HttpStatusCode.BadRequest, $"Category not found, It's Id : {id}");
            }
            context.Categories.Remove(existing);
            await context.SaveChangesAsync();
            logger.LogInformation("DeleteCategory method ended at {DateTime}", DateTime.UtcNow);
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetCategoriesDto>>> GetCategoriesAsync(CategoryFilter filter)
    {
        try
        {
            logger.LogInformation("GetCategories method started at {DateTime}", DateTime.UtcNow);
            var categories = context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                categories = categories.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description))
                categories = categories.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await categories.Select(e => new GetCategoriesDto()
            {
                CategoryId = e.CategoryId,
                Name = e.Name,
                Description = e.Description
            }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var totalRecords = await categories.CountAsync();
            logger.LogInformation("GetCategories method ended at {DateTime}", DateTime.UtcNow);
            return new PagedResponse<List<GetCategoriesDto>>(response, filter.PageNumber, filter.PageSize, totalRecords);
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new PagedResponse<List<GetCategoriesDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCategoriesDto>> GetCategoryById(int id)
    {
        try
        {
            logger.LogInformation("GetCategoryById method started at {DateTime}", DateTime.UtcNow);
            var existing = await context.Categories.Where(x => x.CategoryId == id).Select(e => new GetCategoriesDto()
            {
                CategoryId = e.CategoryId,
                Name = e.Name,
                Description = e.Description
            }).FirstOrDefaultAsync();
            if ( existing == null)
            {
                logger.LogWarning("Category with id {CategoryId} not found, Time: {DateTime}", id, DateTime.UtcNow);
                return new Response<GetCategoriesDto>(HttpStatusCode.BadRequest, $"Category not found, It's Id : {id}");
            }
            logger.LogInformation("GetCategoryById method ended at {DateTime}", DateTime.UtcNow);
            return new Response<GetCategoriesDto>(existing);
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new Response<GetCategoriesDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto categoryDto)
    {
        try
        {
            logger.LogInformation("UpdateCategory method started at {DateTime}", DateTime.UtcNow);
            var existing = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryDto.CategoryId);
            if (existing == null)
            {
                logger.LogWarning("Category with id {CategoryId} not found, Time: {DateTime}", categoryDto.CategoryId, DateTime.UtcNow);
                return new Response<string>(HttpStatusCode.BadRequest, $"Category not found, It's Id : {categoryDto.CategoryId}");
            }
            existing.Name = categoryDto.Name;
            existing.Description = categoryDto.Description;
            await context.SaveChangesAsync();
            logger.LogInformation("UpdateCategory method ended at {DateTime}", DateTime.UtcNow);
            return new Response<string>("Category updated successfully, It's id: " + categoryDto.CategoryId);
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
