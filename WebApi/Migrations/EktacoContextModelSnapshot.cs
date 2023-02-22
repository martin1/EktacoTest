﻿// <auto-generated />
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
                            CreatedAt = new DateTime(2023, 2, 22, 21, 53, 56, 768, DateTimeKind.Local).AddTicks(3030),
                            Name = "Product 1",
                            Price = 0m,
                            PriceWithVat = 0m,
                            ProductGroupId = 1,
                            VatRate = 0m
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 2, 22, 21, 53, 56, 768, DateTimeKind.Local).AddTicks(3030),
                            Name = "Product 2",
                            Price = 0m,
                            PriceWithVat = 0m,
                            ProductGroupId = 1,
                            VatRate = 0m
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
