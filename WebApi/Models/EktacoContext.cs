using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

//dotnet ef migrations add InitialCreate
//dotnet ef database update
public class EktacoContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductGroup> ProductGroups { get; set; } = null!;
    public DbSet<Store> Stores { get; set; } = null!;

    public EktacoContext(DbContextOptions options) : base(options)
    {
        // used by asp.net core - do not remove!
    }

    public IQueryable<ProductGroup> GroupTreeQueryable(int id) => ProductGroups.FromSqlRaw(
            """
                with cte_groups (Id, Name, ParentId) as (
                            select Id, Name, ParentId
                            from ProductGroups
                            where ProductGroups.Id = {0}
                            union all
                            select pg.Id, pg.Name, pg.ParentId
                            from ProductGroups PG 
                            join cte_groups g
                            on g.Id = pg.ParentId
                            )
                            select * from cte_groups
                """, id)
        .AsNoTrackingWithIdentityResolution();

    public IQueryable<GetProductDto> GetProductQueryable() => Products
        .Include(x => x.ProductGroup)
        .Include(x => x.Stores)
        .Select(x => new GetProductDto
        {
            Id = x.Id,
            Name = x.Name,
            GroupName = x.ProductGroup.Name,
            CreatedAt = x.CreatedAt,
            Price = x.Price,
            PriceWithVat = x.PriceWithVat,
            VatRate = x.VatRate,
            Stores = x.Stores.Select(y => new StoreDto { Id = y.Id, Name = y.Name }).ToList()
        })
        .AsNoTrackingWithIdentityResolution();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<Store>()
            .HasMany(x => x.Products)
            .WithMany(x => x.Stores)
            .UsingEntity(j => j.ToTable("StoreProducts"));

        InsertSampleData(builder);
    }

    private static void InsertSampleData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductGroup>().HasData(
            new ProductGroup { Id = 1, Name = "Group 1" },
            new ProductGroup { Id = 2, Name = "Group 2" },
            new ProductGroup { Id = 3, Name = "Group 1-3", ParentId = 1 },
            new ProductGroup { Id = 4, Name = "Group 1-4", ParentId = 1 },
            new ProductGroup { Id = 5, Name = "Group 2-5", ParentId = 2 },
            new ProductGroup { Id = 6, Name = "Group 2-6", ParentId = 2 },
            new ProductGroup { Id = 7, Name = "Group 1-3-7", ParentId = 3 }
        );

        var now = DateTime.Now;
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", ProductGroupId = 1, CreatedAt = now, Price = 10.00m, PriceWithVat = 12.00m, VatRate = 0.2m },
            new Product { Id = 2, Name = "Product 2", ProductGroupId = 1, CreatedAt = now, Price = 5.00m, PriceWithVat = 6.00m, VatRate = 0.2m },
            new Product { Id = 3, Name = "Product 3", ProductGroupId = 2, CreatedAt = now, Price = 7.35m, PriceWithVat = 8.82m, VatRate = 0.2m },
            new Product { Id = 4, Name = "Product 4", ProductGroupId = 3, CreatedAt = now, Price = 102.50m, PriceWithVat = 111.73m, VatRate = 0.09m },
            new Product { Id = 5, Name = "Product 5", ProductGroupId = 4, CreatedAt = now, Price = 24.95m, PriceWithVat = 29.94m, VatRate = 0.2m },
            new Product { Id = 6, Name = "Product 6", ProductGroupId = 5, CreatedAt = now, Price = 2.10m, PriceWithVat = 2.52m, VatRate = 0.2m },
            new Product { Id = 7, Name = "Product 7", ProductGroupId = 6, CreatedAt = now, Price = 15.20m, PriceWithVat = 16.57m, VatRate = 0.09m },
            new Product { Id = 8, Name = "Product 8", ProductGroupId = 7, CreatedAt = now, Price = 50.00m, PriceWithVat = 60.00m, VatRate = 0.2m },
            new Product { Id = 9, Name = "Product 9", ProductGroupId = 7, CreatedAt = now, Price = 35.40m, PriceWithVat = 38.59m, VatRate = 0.09m }
        );

        modelBuilder.Entity<Store>().HasData(
            new Store { Id = 1, Name = "Store 1" },
            new Store { Id = 2, Name = "Store 2" },
            new Store { Id = 3, Name = "Store 3" }
        );

        modelBuilder.Entity<Store>()
            .HasMany(x => x.Products)
            .WithMany(x => x.Stores)
            .UsingEntity(x => x.HasData(
                new { StoresId = 1, ProductsId = 1 },
                new { StoresId = 1, ProductsId = 2 },
                new { StoresId = 2, ProductsId = 1 },
                new { StoresId = 2, ProductsId = 3 },
                new { StoresId = 2, ProductsId = 4 },
                new { StoresId = 3, ProductsId = 1 },
                new { StoresId = 3, ProductsId = 5 },
                new { StoresId = 3, ProductsId = 6 },
                new { StoresId = 3, ProductsId = 7 }
            ));
    }
}

public class Product
{
    [Key] public int Id { get; set; }
    public int ProductGroupId { get; set; }
    public ProductGroup ProductGroup { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal PriceWithVat { get; set; }
    public decimal VatRate { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Store> Stores { get; set; } = new();
}

public class ProductGroup
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public ProductGroup? Parent { get; set; }
    public List<ProductGroup> SubGroups { get; set; } = new();
    public List<Product> Products { get; set; } = new();
}

public class Store
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Product> Products { get; set; } = new();
}