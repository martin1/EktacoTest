using WebApi.Models;

namespace WebApi.Services.Interfaces;

public interface IProductService
{
    /// <summary>
    /// Returns a product by its ID or null if not found
    /// </summary>
    Task<GetProductDto?> GetAsync(int id);

    /// <summary>
    /// Returns all products
    /// </summary>
    Task<List<GetProductDto>> GetAllAsync();

    /// <summary>
    /// Creates a new product. If successful, returns its ID, otherwise returns an error.
    /// </summary>
    Task<(int AddedProductId, AddProductError Error)> TryAddAsync(AddProductDto product);

    /// <summary>
    /// Returns products that are sold in a particular store
    /// </summary>
    Task<List<StoreProductDto>> GetByStore(int storeId);
}