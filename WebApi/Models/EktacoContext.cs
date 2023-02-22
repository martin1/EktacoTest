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
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Store>()
            .HasMany(x => x.Products)
            .WithMany(x => x.Stores)
            .UsingEntity(j => j.ToTable("StoreProducts"));
        
        
        modelBuilder.Entity<ProductGroup>().HasData(
            new ProductGroup { Id = 1, Name = "Group 1" },
            new ProductGroup { Id = 2, Name = "Group 2" },
            new ProductGroup { Id = 3, Name = "Group 1-3", ParentId = 1 },
            new ProductGroup { Id = 4, Name = "Group 1-4", ParentId = 1 },
            new ProductGroup { Id = 5, Name = "Group 2-5", ParentId = 2 },
            new ProductGroup { Id = 6, Name = "Group 2-6", ParentId = 2 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Product 1", ProductGroupId = 1, },
            new Product { Id = 2, Name = "Product 2", ProductGroupId = 1, }
        );

        modelBuilder.Entity<Store>().HasData(
            new Store {Id = 1, Name = "Store 1"},
            new Store {Id = 2, Name = "Store 2"},
            new Store {Id = 3, Name = "Store 3"}
        );

        modelBuilder.Entity<Store>()
            .HasMany(x => x.Products)
            .WithMany(x => x.Stores)
            .UsingEntity(x => x.HasData(new { StoresId = 1, ProductsId = 1 }));
        
        modelBuilder.Entity<Store>()
            .HasMany(x => x.Products)
            .WithMany(x => x.Stores)
            .UsingEntity(x => x.HasData(new { StoresId = 1, ProductsId = 2 }));
    }
}

public class Store
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Product> Products { get; set; } = new();
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
