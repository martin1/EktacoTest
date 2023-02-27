using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ProductGroupService : IProductGroupService
{
    private readonly EktacoContext _db;

    public ProductGroupService(EktacoContext db) => _db = db;

    public async Task<List<ProductGroupDto>> GetTreeAsync(int? id)
    {
        var groups = id > 0
            ? await _db.GroupTreeQueryable(id.Value).Select(x => ToDto(x)).ToListAsync()
            : await _db.ProductGroups.Select(x => ToDto(x)).ToListAsync();

        if (!groups.Any()) return new();
        
        var sgDict = groups.Where(x => x.ParentId is not null)
            .GroupBy(x => x.ParentId!.Value)
            .ToDictionary(x => x.Key, x => x.ToList());

        return groups.Where(x => x.Id > 0 && x.Id == id || x.ParentId is null)
            .Select(x => PopulateSubgroups(x, sgDict))
            .ToList();
    }

    private static ProductGroupDto ToDto(ProductGroup pg) => new()
    {
        Id = pg.Id,
        ParentId = pg.ParentId,
        Name = pg.Name,
        Subgroups = new()
    };

    private static ProductGroupDto PopulateSubgroups(ProductGroupDto pg, IReadOnlyDictionary<int, List<ProductGroupDto>> d)
    {
        if (!d.TryGetValue(pg.Id, out var subgroups)) return pg;

        foreach (var sg in subgroups)
        {
            pg.Subgroups.Add(sg);
            PopulateSubgroups(sg, d);
        }

        return pg;
    }
}