using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ProductGroupService : IProductGroupService
{
    private readonly EktacoContext _db;

    public ProductGroupService(EktacoContext db) => _db = db;

    public async Task<ProductGroupDto?> GetTree(int id)
    {
        if (id <= 0) return null;
        var groups = await _db.GetGroupHierarchy(id);
        return BuildTree(id, groups);
    }

    private static ProductGroupDto? BuildTree(int id, List<ProductGroupDto> groups)
    {
        var g = groups.FirstOrDefault(x => x.Id == id);
        if (g is null) return null;

        var d = groups.Where(x => x.Id != id)
            .GroupBy(x => x.ParentId!.Value)
            .ToDictionary(x => x.Key, x => x.ToList());

        PopulateSubgroups(g, d);

        return g;
    }

    private static void PopulateSubgroups(ProductGroupDto pg, IReadOnlyDictionary<int, List<ProductGroupDto>> d)
    {
        if (!d.TryGetValue(pg.Id, out var subgroups)) return;

        foreach (var sg in subgroups)
        {
            pg.Subgroups.Add(sg);
            PopulateSubgroups(sg, d);
        }
    }
}