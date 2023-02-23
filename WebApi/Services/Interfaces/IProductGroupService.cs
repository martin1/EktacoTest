using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IProductGroupService
{
    /// <summary>
    /// Return product group and its subgroups as a tree
    /// </summary>
    public Task<ProductGroupDto?> GetTree(int id);
}