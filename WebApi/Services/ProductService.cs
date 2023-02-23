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
    public async Task<int> AddAsync(AddProductDto p)
    {
        var product = await ValidateAsync(p);
        if (product is null) return 0;

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }
    
    private async Task<Product?> ValidateAsync(AddProductDto p)
    {
        if (p.Price <= 0 || p.ProductGroupId <= 0) return null;

        decimal price;
        decimal priceWithVat;
        decimal vatRate;
        
        var priceValues = new[] { p.PriceWithVat, p.Price, p.VatRate }.Count(x => x > 0);
        switch (priceValues)
        {
            case < 2:
                return null;
            case 2:
                switch (p)
                {
                    case { PriceWithVat: > 0, Price: > 0 }:
                        priceWithVat = Round(p.PriceWithVat.Value);
                        price = Round(p.Price.Value);
                        vatRate = Round((priceWithVat - price) / price);
                        break;
                    case { Price: > 0, VatRate: > 0 }:
                        price = Round(p.Price.Value);
                        vatRate = Round(p.VatRate.Value);
                        priceWithVat = Round(price * (1 + vatRate));
                        break;
                    case { PriceWithVat: > 0, VatRate: > 0 }:
                        priceWithVat = Round(p.PriceWithVat.Value);
                        vatRate = Round(p.VatRate.Value);
                        price = priceWithVat / (1 + vatRate);
                        break;
                    default:
                        throw new ArgumentException(nameof(p));
                }
                break;
            case > 2:
            {
                price = Round(p.Price!.Value);
                vatRate = Round(p.VatRate!.Value);
                var expectedPriceWithVat = Round(p.PriceWithVat!.Value);
            
                var calculatedPriceWithVat = Round(price * (1 + vatRate));
                if (calculatedPriceWithVat != expectedPriceWithVat) return null;

                priceWithVat = calculatedPriceWithVat;
                break;
            }
        }

        var productGroup = await _context.ProductGroups.FindAsync(p.ProductGroupId);
        if (productGroup is null) return null;

        List<Store> stores = new();
        if (p.StoreIds.Any())
        {
            stores = await _context.Stores.Where(x => p.StoreIds.Contains(x.Id)).ToListAsync();
            if (stores.Count < p.StoreIds.Count) return null;
        }

        return new Product
        {
            Name = p.Name,
            Price = price,
            PriceWithVat = priceWithVat,
            VatRate = vatRate,
            ProductGroup = productGroup,
            CreatedAt = DateTime.Now,
            Stores = stores
        };
    }

    private static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}