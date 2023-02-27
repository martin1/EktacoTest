using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductGroupController : ControllerBase
{
    private readonly IProductGroupService _productGroupService;

    public ProductGroupController(IProductGroupService productGroupService) =>
        _productGroupService = productGroupService;

    [HttpGet]
    public async Task<ActionResult<List<ProductGroupDto>>> GetTree(int? id)
    {
        if (id <= 0) return NotFound();
        
        var groups = await _productGroupService.GetTreeAsync(id);
        return groups.Any() ? groups : NotFound();
    }
}