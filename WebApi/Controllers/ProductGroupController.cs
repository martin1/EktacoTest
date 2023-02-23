using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductGroupController : ControllerBase
{
    private readonly IProductGroupService _productGroupService;

    public ProductGroupController(IProductGroupService productGroupService) => _productGroupService = productGroupService;

    [HttpGet]
    public async Task<ActionResult<ProductGroupDto>> GetTree(int id)
    {
        var g = await _productGroupService.GetTree(id);
        if (g is null) return NotFound();

        return g;
    }
}