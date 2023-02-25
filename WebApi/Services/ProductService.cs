using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ProductService : IProductService
{
    private readonly EktacoContext _db;
    
    public ProductService(EktacoContext db) =>  _db = db;

    public async Task<Product?> FindAsync(int id) => await _db.Products
        .Include(x => x.ProductGroup)
        .Include(x => x.Stores)
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Product>> FindAllAsync() => await _db.Products
        .Include(x => x.ProductGroup)
        .Include(x => x.Stores)
        .ToListAsync();
    
    public async Task<(int AddedProductId, AddProductError Error)> TryAddAsync(AddProductDto p)
    {
        var (product, error) = await ValidateAsync(p);

        if (error is not AddProductError.None || product is null) return (0, error);

        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
        return (product.Id, AddProductError.None);
    }
    
    private async Task<(Product? Product, AddProductError Error)> ValidateAsync(AddProductDto p)
    {
        if (p is not { ProductGroupId: > 0 })
        {
            return (null, AddProductError.ProductGroupInvalid);
        }

        if (p.Price <= 0 || p.PriceWithVat <= 0 || p.VatRate <= 0)
        {
            return (null, AddProductError.PriceVatValuesInvalid);
        }

        decimal price;
        decimal priceWithVat;
        decimal vatRate;
        
        var priceValues = new[] { p.PriceWithVat, p.Price, p.VatRate }.Count(x => x > 0);
        switch (priceValues)
        {
            case < 2:
                return (null, AddProductError.PriceVatValuesInvalid);
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
                if (calculatedPriceWithVat != expectedPriceWithVat) return (null, AddProductError.PriceVatValuesInvalid);

                priceWithVat = calculatedPriceWithVat;
                break;
            }
        }

        var productGroup = await _db.ProductGroups.FindAsync(p.ProductGroupId);
        if (productGroup is null) return (null, AddProductError.ProductGroupInvalid);

        List<Store> stores = new();
        if (p.StoreIds.Any())
        {
            stores = await _db.Stores.Where(x => p.StoreIds.Contains(x.Id)).ToListAsync();
            if (stores.Count < p.StoreIds.Count) return (null, AddProductError.StoresInvalid);
        }

        var product = new Product
        {
            Name = p.Name,
            Price = price,
            PriceWithVat = priceWithVat,
            VatRate = vatRate,
            ProductGroup = productGroup,
            CreatedAt = DateTime.Now,
            Stores = stores
        };
        return (product, AddProductError.None);
    }

    private static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}