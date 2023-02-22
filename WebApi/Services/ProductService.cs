using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ProductService : IProductService
{
    private readonly EktacoContext _context;
    
    public ProductService(EktacoContext context)
    {
        _context = context;
    }

    public async Task<Product?> FindAsync(int id) => await _context.Products.FindAsync(id);

    public async Task<List<Product>> FindAllAsync() => await _context.Products.ToListAsync();
}