using Domain.Constants;
using Domain.DTOs.ProductDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Permissions;
using Infrastructure.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController(IProductService productService):ControllerBase
{
    [HttpPost("Add-Product")]
    [PermissionAuthorize(Permissions.Products.Create)]
    public async Task<Response<string>> AddProductAsync([FromForm] AddProductDto addProductDto)
    {
        return await productService.AddProductAsync(addProductDto);
    }
    [HttpGet("Get-Products")]
    [PermissionAuthorize(Permissions.Products.View)]
    public async Task<PagedResponse<List<GetProductsDto>>> GetProductsAsync([FromQuery] ProductFilter filter)
    {
        return await productService.GetProductsAsync(filter);
    }
    [HttpGet("Get-Product-By-Id")]
    [PermissionAuthorize(Permissions.Products.View)]
    public async Task<Response<GetProductsDto>> GetProductByIdAsync([FromQuery]int id)
    {
        return await productService.GetProductByIdAsync(id);
    }
    [HttpPut("Update-Product")]
    [PermissionAuthorize(Permissions.Products.Edit)]
    public async Task<Response<string>> UpdateProductAsync([FromForm]UpdateProductDto updateProductDto)
    {
        return await productService.UpdateProductAsync(updateProductDto);
    }
    [HttpDelete("Delete-Product")]
    [PermissionAuthorize(Permissions.Products.Delete)]
    public async Task<Response<bool>> DeleteProductAsync(int id)
    {
        return await productService.DeleteProductAsync(id);
    }
}
