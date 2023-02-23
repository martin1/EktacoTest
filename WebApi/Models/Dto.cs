using Microsoft.Extensions.Options;

namespace WebApi.Models;

public class ProductBaseDto
{
    public string Name { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public decimal? Price { get; init; }
    public decimal? PriceWithVat { get; init; }
    public decimal? VatRate { get; init; }
}

public class GetProductDto : ProductBaseDto
{
    public string ProductGroupName { get; init; } = null!;
    public List<StoreDto> Stores { get; init; } = new();
}

public class AddProductDto : ProductBaseDto
{
    public int ProductGroupId { get; init; }
    public List<int> StoreIds { get; init; } = new();
}

public record StoreDto(string Name);
