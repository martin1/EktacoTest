using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IProductGroupService
{
    /// <summary>
    /// Returns product group and its subgroups as a tree
    /// If id is not provided then returns all product groups as a tree
    /// </summary>
    public Task<List<ProductGroupDto>> GetTreeAsync(int? id);
}