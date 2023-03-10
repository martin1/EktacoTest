// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Models;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(EktacoContext))]
    partial class EktacoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StoresId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductsId", "StoresId");

                    b.HasIndex("StoresId");

                    b.ToTable("StoreProducts", (string)null);

                    b.HasData(
                        new
                        {
                            ProductsId = 1,
                            StoresId = 1
                        },
                        new
                        {
                            ProductsId = 2,
                            StoresId = 1
                        },
                        new
                        {
                            ProductsId = 1,
                            StoresId = 2
                        },
                        new
                        {
                            ProductsId = 3,
                            StoresId = 2
                        },
                        new
                        {
                            ProductsId = 4,
                            StoresId = 2
                        },
                        new
                        {
                            ProductsId = 1,
                            StoresId = 3
                        },
                        new
                        {
                            ProductsId = 5,
                            StoresId = 3
                        },
                        new
                        {
                            ProductsId = 6,
                            StoresId = 3
                        },
                        new
                        {
                            ProductsId = 7,
                            StoresId = 3
                        });
                });

            modelBuilder.Entity("WebApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PriceWithVat")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("VatRate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductGroupId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 1",
                            Price = 10.00m,
                            PriceWithVat = 12.00m,
                            ProductGroupId = 1,
                            VatRate = 0.2m
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 2",
                            Price = 5.00m,
                            PriceWithVat = 6.00m,
                            ProductGroupId = 1,
                            VatRate = 0.2m
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 3",
                            Price = 7.35m,
                            PriceWithVat = 8.82m,
                            ProductGroupId = 2,
                            VatRate = 0.2m
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 4",
                            Price = 102.50m,
                            PriceWithVat = 111.73m,
                            ProductGroupId = 3,
                            VatRate = 0.09m
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 5",
                            Price = 24.95m,
                            PriceWithVat = 29.94m,
                            ProductGroupId = 4,
                            VatRate = 0.2m
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 6",
                            Price = 2.10m,
                            PriceWithVat = 2.52m,
                            ProductGroupId = 5,
                            VatRate = 0.2m
                        },
                        new
                        {
                            Id = 7,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 7",
                            Price = 15.20m,
                            PriceWithVat = 16.57m,
                            ProductGroupId = 6,
                            VatRate = 0.09m
                        },
                        new
                        {
                            Id = 8,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 8",
                            Price = 50.00m,
                            PriceWithVat = 60.00m,
                            ProductGroupId = 7,
                            VatRate = 0.2m
                        },
                        new
                        {
                            Id = 9,
                            CreatedAt = new DateTime(2023, 2, 27, 20, 42, 0, 855, DateTimeKind.Local).AddTicks(5529),
                            Name = "Product 9",
                            Price = 35.40m,
                            PriceWithVat = 38.59m,
                            ProductGroupId = 7,
                            VatRate = 0.09m
                        });
                });

            modelBuilder.Entity("WebApi.Models.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Group 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Group 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Group 1-3",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "Group 1-4",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 5,
                            Name = "Group 2-5",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 6,
                            Name = "Group 2-6",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 7,
                            Name = "Group 1-3-7",
                            ParentId = 3
                        });
                });

            modelBuilder.Entity("WebApi.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Store 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Store 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Store 3"
                        });
                });

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.HasOne("WebApi.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Store", null)
                        .WithMany()
                        .HasForeignKey("StoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.Product", b =>
                {
                    b.HasOne("WebApi.Models.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("WebApi.Models.ProductGroup", b =>
                {
                    b.HasOne("WebApi.Models.ProductGroup", "Parent")
                        .WithMany("SubGroups")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("WebApi.Models.ProductGroup", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
