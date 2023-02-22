using Microsoft.AspNetCore.Mvc;
using WebApi.Models.DTO;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProduct(int? id)
        {
            switch (id)
            {
                case null:
                {
                    var products = await _productService.FindAllAsync();
                    return products.Select(x => new ProductDto(
                        Name: x.Name,
                        ProductGroupName: x.ProductGroup.Name,
                        CreatedAt: x.CreatedAt,
                        Price: x.Price,
                        PriceWithVat: x.PriceWithVat,
                        VatRate: x.VatRate,
                        Stores: x.Stores.Select(y => new StoreDto(y.Name)).ToList()
                    )).ToList();
                }
                case <= 0:
                    return NotFound();
            }

            var p = await _productService.FindAsync(id.Value);
            if (p is null) return NotFound();

            return new List<ProductDto>
            {
                new(
                    Name: p.Name, 
                    ProductGroupName: p.ProductGroup.Name, 
                    CreatedAt: p.CreatedAt, 
                    Price: p.Price, 
                    PriceWithVat: p.PriceWithVat, 
                    VatRate:p.VatRate, 
                    Stores: p.Stores.Select(x => new StoreDto(x.Name)).ToList()
                )
            };
        }
    }
}