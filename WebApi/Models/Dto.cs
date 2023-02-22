namespace WebApi.Models.DTO;

public record ProductDto(string Name, string ProductGroupName, DateTime CreatedAt, decimal Price, decimal PriceWithVat, decimal VatRate, List<StoreDto> Stores);

public record StoreDto(string Name);