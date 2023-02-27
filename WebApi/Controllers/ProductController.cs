using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService) => _productService = productService;

    [HttpGet]
    public async Task<ActionResult<List<GetProductDto>>> GetProduct(int? id)
    {
        switch (id)
        {
            case null:
            {
                return await _productService.GetAllAsync();
            }
            case <= 0:
                return NotFound();
        }

        var p = await _productService.GetAsync(id.Value);
        return p is null
            ? NotFound()
            : new List<GetProductDto> { p };
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct(AddProductDto p)
    {
        var (id, error) = await _productService.TryAddAsync(p);
        return error is AddProductError.None
            ? Ok(new { Id = id })
            : UnprocessableEntity(error.ToString());
    }

    [HttpGet("Store")]
    public async Task<ActionResult<List<StoreProductDto>>> GetByStore(int storeId)
    {
        return storeId <= 0
            ? NotFound()
            : await _productService.GetByStore(storeId);
    }
}