using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ProductGroupService : IProductGroupService
{
    private readonly EktacoContext _db;

    public ProductGroupService(EktacoContext db) => _db = db;

    public async Task<ProductGroupDto?> GetTree(int id)
    {
        var g = await Get(id);
        return g is null ? null : ToDto(g);
    }

    private static ProductGroupDto ToDto(ProductGroup pg) => new ProductGroupDto
    {
        Id = pg.Id,
        ParentId = pg.ParentId,
        Name = pg.Name,
        Subgroups = pg.SubGroups.Select(ToDto).ToList()
    };

    private async Task<ProductGroup?> Get(int id) => await _db.ProductGroups
        .Include(x => x.Parent)
        .Include(x => x.SubGroups)
        .FirstOrDefaultAsync(x => x.Id == id);
}