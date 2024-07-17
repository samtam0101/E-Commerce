using System.Net;
using Domain.DTOs.ProductDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.ProductService;

public class ProductService(DataContext context, ILogger<ProductService> logger, IFileService fileService) : IProductService
{
    public async Task<Response<string>> AddProductAsync(AddProductDto addProductDto)
    {
        try
        {
            logger.LogInformation("AddProduct method started at {DateTime}", DateTime.UtcNow);
            var existingProduct = await context.Products.AnyAsync(x => x.Name == addProductDto.Name);
            if (existingProduct)
            {
                logger.LogWarning("Product with name {ProductName} already exists, Time: {DateTime}", addProductDto.Name, DateTime.UtcNow);
                return new Response<string>(HttpStatusCode.BadRequest, "Product with this name already exists");
            }
            var product = new Product()
            {
                CategoryId = addProductDto.CategoryId,
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                Price = addProductDto.Price,
                CreatedDate = DateTime.UtcNow,
                ImageUrl = addProductDto.ImageUrl == null ? "null" : await fileService.CreateFile(addProductDto.ImageUrl)
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            logger.LogInformation("AddProduct method ended at {DateTime}", DateTime.UtcNow);
            return new Response<string>($"Product created successfully, It's id: {product.ProductId}");
        }
        catch (Exception e)
        {
            logger.LogError("AddProduct method failed at {DateTime}", DateTime.UtcNow);
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteProductAsync(int id)
    {
        try
        {
            logger.LogInformation("DeleteProduct method started at {DateTime}", DateTime.UtcNow);
            var existing = await context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            if (existing == null)
            {
                logger.LogWarning("Product with id {ProductId} not found, Time: {DateTime}", id, DateTime.UtcNow);
                return new Response<bool>(HttpStatusCode.BadRequest, $"Product not found, It's Id : {id}");
            }
            if (existing.ImageUrl != null)
            {
                fileService.DeleteFile(existing.ImageUrl);
            }
            context.Products.Remove(existing);
            await context.SaveChangesAsync();
            logger.LogInformation("DeleteProduct method ended at {DateTime}", DateTime.UtcNow);
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new PagedResponse<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetProductsDto>> GetProductByIdAsync(int id)
    {
        try
        {
            logger.LogInformation("GetProductById method started at {DateTime}", DateTime.UtcNow);
            var existing = await context.Products.Where(x => x.ProductId == id).Select(e => new GetProductsDto()
            {
                ProductId = e.ProductId,
                CategoryId = e.CategoryId,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                ImageUrl = e.ImageUrl,
                CreatedDate = e.CreatedDate,
                UpdatedDate = e.UpdatedDate
            }).FirstOrDefaultAsync();
            if (existing is null)
            {
                logger.LogWarning("Product with id {ProductId} not found, Time: {DateTime}", id, DateTime.UtcNow);
                return new Response<GetProductsDto>(HttpStatusCode.BadRequest, $"Product not found, It's Id : {id}");
            }

            logger.LogInformation("GetProductById method ended at {DateTime}", DateTime.UtcNow);
            return new Response<GetProductsDto>(existing);
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new Response<GetProductsDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetProductsDto>>> GetProductsAsync(ProductFilter filter)
    {
        try
        {
            logger.LogInformation("GetProducts method started at {DateTime}", DateTime.UtcNow);
            var products = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                products = products.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
            if (!string.IsNullOrEmpty(filter.Description))
                products = products.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));

            var response = await products.Select(e => new GetProductsDto()
            {
                ProductId = e.ProductId,
                CategoryId = e.CategoryId,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                ImageUrl = e.ImageUrl,
                CreatedDate = e.CreatedDate,
                UpdatedDate = e.UpdatedDate
            }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var totalRecords = await products.CountAsync();
            logger.LogInformation("GetProducts method ended at {DateTime}", DateTime.UtcNow);
            return new PagedResponse<List<GetProductsDto>>(response, totalRecords, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new PagedResponse<List<GetProductsDto>>(HttpStatusCode.InternalServerError, e.Message);
        }

    }

    public async Task<Response<string>> UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        try
        {
            logger.LogInformation("UpdateProduct method started at {DateTime}", DateTime.UtcNow);
            var existing = await context.Products.FirstOrDefaultAsync(x => x.ProductId == updateProductDto.ProductId);
            if (existing == null)
            {
                logger.LogWarning("Product with id {ProductId} not found, Time: {DateTime}", updateProductDto.ProductId, DateTime.UtcNow);
                return new Response<string>(HttpStatusCode.BadRequest, $"Product not found, It's Id : {updateProductDto.ProductId}");
            }
            if (updateProductDto.ImageUrl != null)
            {
                if (existing.ImageUrl != null)
                {
                    fileService.DeleteFile(existing.ImageUrl);
                    existing.ImageUrl = await fileService.CreateFile(updateProductDto.ImageUrl);
                }
            }
            existing.CategoryId = updateProductDto.CategoryId;
            existing.Name = updateProductDto.Name;
            existing.Description = updateProductDto.Description;
            existing.Price = updateProductDto.Price;
            existing.UpdatedDate = DateTime.UtcNow;
            await context.SaveChangesAsync();
            logger.LogInformation("UpdateProduct method ended at {DateTime}", DateTime.UtcNow);
            return new Response<string>($"Product updated successfully, It's id: {updateProductDto.ProductId}");
        }
        catch (Exception e)
        {
            logger.LogError("Exception {Exception}, time={DateTimeNow}", e.Message, DateTimeOffset.UtcNow);
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}