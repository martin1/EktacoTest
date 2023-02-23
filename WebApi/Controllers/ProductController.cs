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
                var products = await _productService.FindAllAsync();
                return products.Select(x => new GetProductDto
                {
                    Name = x.Name,
                    ProductGroupName = x.ProductGroup.Name,
                    CreatedAt = x.CreatedAt,
                    Price = x.Price,
                    PriceWithVat = x.PriceWithVat,
                    VatRate = x.VatRate,
                    Stores = x.Stores.Select(y => y.Name).ToList()
                }).ToList();
            }
            case <= 0:
                return NotFound();
        }

        var p = await _productService.FindAsync(id.Value);
        if (p is null) return NotFound();

        return new List<GetProductDto>
        {
            new()
            {
                Name = p.Name,
                ProductGroupName = p.ProductGroup.Name,
                CreatedAt = p.CreatedAt,
                Price = p.Price,
                PriceWithVat = p.PriceWithVat,
                VatRate = p.VatRate,
                Stores = p.Stores.Select(x => x.Name).ToList()
            }
        };
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct(AddProductDto p)
    {
        var id = await _productService.AddAsync(p);
        var okResp = new { Id = id };
        return id > 0 ? Ok(okResp) : UnprocessableEntity();
    }
}