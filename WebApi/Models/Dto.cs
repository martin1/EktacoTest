namespace WebApi.Models;

public class ProductDtoBase
{
    public string Name { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public decimal? Price { get; init; }
    public decimal? PriceWithVat { get; init; }
    public decimal? VatRate { get; init; }
}

public class GetProductDto : ProductDtoBase
{
    public int Id { get; set; }
    public string GroupName { get; init; } = null!;
    public List<StoreDto> Stores { get; init; } = new();
}

public class StoreDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

public class AddProductDto : ProductDtoBase
{
    public int GroupId { get; init; }
    public List<int> StoreIds { get; } = new();
}

public class ProductGroupDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public int? ParentId { get; init; }
    public List<ProductGroupDto> Subgroups { get; init; } = new();
}

public class StoreProductDto : ProductDtoBase
{
    public int Id { get; set; }
    public string GroupName { get; init; } = null!;
}

public enum AddProductError
{
    None,
    NameInvalid,
    ProductGroupInvalid,
    PriceVatValuesInvalid,
    StoresInvalid,
}