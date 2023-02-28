using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ProductService : IProductService
{
    private const decimal MaxSupportedVatRate = 0.2m;

    private readonly EktacoContext _db;

    public ProductService(EktacoContext db) => _db = db;

    public async Task<GetProductDto?> GetAsync(int id) => await _db.GetProductQueryable().SingleOrDefaultAsync(x => x.Id == id);

    public async Task<List<GetProductDto>> GetAllAsync() => await _db.GetProductQueryable().ToListAsync();

    public async Task<(int AddedProductId, AddProductError Error)> TryAddAsync(AddProductDto p)
    {
        var (product, error) = await ValidateAsync(p);

        if (error is not AddProductError.None || product is null) return (0, error);

        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
        return (product.Id, AddProductError.None);
    }

    public Task<List<StoreProductDto>> GetByStore(int storeId) =>
        _db.Products
            .Where(x => x.Stores.Select(y => y.Id).Contains(storeId))
            .Select(x => new StoreProductDto
            {
                Id = x.Id,
                VatRate = x.VatRate,
                PriceWithVat = x.PriceWithVat,
                Price = x.Price,
                Name = x.Name,
                GroupName = x.ProductGroup.Name,
                CreatedAt = x.CreatedAt
            }).ToListAsync();

    private async Task<(Product? Product, AddProductError Error)> ValidateAsync(AddProductDto p)
    {
        if (string.IsNullOrWhiteSpace(p.Name))
        {
            return (null, AddProductError.NameInvalid);
        }

        if (p.GroupId <= 0)
        {
            return (null, AddProductError.ProductGroupInvalid);
        }

        if (p.StoreIds.Any() && !p.StoreIds.All(x => x > 0))
        {
            return (null, AddProductError.StoresInvalid);
        }

        if (p.Price <= 0 || p.PriceWithVat <= 0 || p.VatRate < 0 || p.VatRate > MaxSupportedVatRate)
        {
            return (null, AddProductError.PriceVatValuesInvalid);
        }

        decimal price;
        decimal priceWithVat;
        decimal vatRate;

        var priceVatValues = new[] { p.PriceWithVat, p.Price, p.VatRate }.Count(x => x >= 0);
        switch (priceVatValues)
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
                    case { Price: > 0, VatRate: >= 0 }:
                        price = Round(p.Price.Value);
                        vatRate = Round(p.VatRate.Value);
                        priceWithVat = Round(price * (1 + vatRate));
                        break;
                    case { PriceWithVat: > 0, VatRate: >= 0 }:
                        priceWithVat = Round(p.PriceWithVat.Value);
                        vatRate = Round(p.VatRate.Value);
                        price = Round(priceWithVat / (1 + vatRate));
                        break;
                    default:
                        return (null, AddProductError.PriceVatValuesInvalid);
                }

                break;
            case > 2:
            {
                price = Round(p.Price!.Value);
                vatRate = Round(p.VatRate!.Value);
                priceWithVat = Round(p.PriceWithVat!.Value);

                var calculatedPriceWithVat = Round(price * (1 + vatRate));
                if (calculatedPriceWithVat != priceWithVat)
                {
                    return (null, AddProductError.PriceVatValuesInvalid);
                }

                break;
            }
        }

        var productGroup = await _db.ProductGroups.FindAsync(p.GroupId);
        if (productGroup is null) return (null, AddProductError.ProductGroupInvalid);

        var stores = p.StoreIds.Any()
            ? await _db.Stores.Where(x => p.StoreIds.Contains(x.Id)).ToListAsync()
            : new List<Store>();

        if (stores.Count < p.StoreIds.Count) return (null, AddProductError.StoresInvalid);

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