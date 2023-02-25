using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IProductService
{
    /// <summary>
    /// Returns a product by its ID or null if not found
    /// </summary>
    Task<Product?> FindAsync(int id);

    /// <summary>
    /// Returns all products
    /// </summary>
    Task<List<Product>> FindAllAsync();

    /// <summary>
    /// Creates a new product. If successful, returns its ID, otherwise returns an error.
    /// </summary>
    Task<(int AddedProductId, AddProductError Error)> TryAddAsync(AddProductDto product);
}