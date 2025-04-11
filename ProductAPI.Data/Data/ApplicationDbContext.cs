using ProductAPI.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Data.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductStore> ProductStores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure many-to-many relationship between Product and Store
        modelBuilder.Entity<ProductStore>().HasKey(ps => ps.Id);

        modelBuilder
            .Entity<ProductStore>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.ProductStores)
            .HasForeignKey(ps => ps.ProductId);

        modelBuilder
            .Entity<ProductStore>()
            .HasOne(ps => ps.Store)
            .WithMany(s => s.ProductStores)
            .HasForeignKey(ps => ps.StoreId);

        // Configure hierarchical relationship between ProductGroup and its subgroups
        modelBuilder
            .Entity<ProductGroup>()
            .HasOne(pg => pg.ParentGroup)
            .WithMany(pg => pg.ChildGroups)
            .HasForeignKey(pg => pg.ParentGroupId);

        // Ensure that each Product belongs to a ProductGroup
        modelBuilder
            .Entity<Product>()
            .HasOne(p => p.ProductGroup)
            .WithMany(pg => pg.Products)
            .HasForeignKey(p => p.ProductGroupId)
            .IsRequired(); // Products without a group are not supported

        // Specify precision and scale for decimal properties
        modelBuilder.Entity<Product>().Property(p => p.SalePrice).HasPrecision(18, 2); // Adjust precision and scale as needed

        modelBuilder.Entity<Product>().Property(p => p.SalePriceWithVAT).HasPrecision(18, 2); // Adjust precision and scale as needed

        modelBuilder.Entity<Product>().Property(p => p.VATRate).HasPrecision(5, 2); // Adjust precision and scale as needed

        // Seed data for ProductGroups table
        modelBuilder
            .Entity<ProductGroup>()
            .HasData(
                new ProductGroup
                {
                    ProductGroupId = 1,
                    ProductGroupName = "FOOD",
                    ParentGroupId = null
                },
                new ProductGroup
                {
                    ProductGroupId = 2,
                    ProductGroupName = "DRINK",
                    ParentGroupId = null
                },
                new ProductGroup
                {
                    ProductGroupId = 3,
                    ProductGroupName = "DESSERT",
                    ParentGroupId = 1
                },
                new ProductGroup
                {
                    ProductGroupId = 4,
                    ProductGroupName = "MAIN COURSE",
                    ParentGroupId = 1
                },
                new ProductGroup
                {
                    ProductGroupId = 5,
                    ProductGroupName = "SOFT DRINK",
                    ParentGroupId = 2
                },
                new ProductGroup
                {
                    ProductGroupId = 6,
                    ProductGroupName = "ALCOHOL",
                    ParentGroupId = 2
                },
                new ProductGroup
                {
                    ProductGroupId = 7,
                    ProductGroupName = "BEER",
                    ParentGroupId = 6
                },
                new ProductGroup
                {
                    ProductGroupId = 8,
                    ProductGroupName = "LIQOUR",
                    ParentGroupId = 6
                }
            );

        // Seed data for Stores table
        modelBuilder
            .Entity<Store>()
            .HasData(
                new Store { StoreId = 1, StoreName = "Main Warehouse" },
                new Store { StoreId = 2, StoreName = "Bar Store" },
                new Store { StoreId = 3, StoreName = "Kitchen Store" }
            );

        // Seed data for Products table
        modelBuilder
            .Entity<Product>()
            .HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Cheeseburger",
                    DateAdded = new DateTime(2025, 4, 8, 18, 59, 5),
                    SalePrice = 9.52m,
                    SalePriceWithVAT = 11.61m,
                    VATRate = 22.00m,
                    ProductGroupId = 4
                },
                new Product
                {
                    ProductId = 2,
                    ProductName = "Mafioso Pizza",
                    DateAdded = new DateTime(2025, 4, 6, 18, 59, 5),
                    SalePrice = 15.00m,
                    SalePriceWithVAT = 18.30m,
                    VATRate = 22.00m,
                    ProductGroupId = 4
                },
                new Product
                {
                    ProductId = 3,
                    ProductName = "Must Munk",
                    DateAdded = new DateTime(2025, 4, 6, 18, 59, 5),
                    SalePrice = 4.10m,
                    SalePriceWithVAT = 5.00m,
                    VATRate = 22.00m,
                    ProductGroupId = 7
                },
                new Product
                {
                    ProductId = 4,
                    ProductName = "Cocca Cola 2L",
                    DateAdded = new DateTime(2025, 4, 8, 18, 59, 5),
                    SalePrice = 6.56m,
                    SalePriceWithVAT = 8.00m,
                    VATRate = 22.00m,
                    ProductGroupId = 5
                }
            );

        // Seed data for ProductStores table
        modelBuilder
            .Entity<ProductStore>()
            .HasData(
                new ProductStore
                {
                    Id = 1,
                    ProductId = 1,
                    StoreId = 1
                },
                new ProductStore
                {
                    Id = 2,
                    ProductId = 1,
                    StoreId = 3
                },
                new ProductStore
                {
                    Id = 3,
                    ProductId = 2,
                    StoreId = 3
                },
                new ProductStore
                {
                    Id = 4,
                    ProductId = 3,
                    StoreId = 2
                },
                new ProductStore
                {
                    Id = 5,
                    ProductId = 4,
                    StoreId = 1
                },
                new ProductStore
                {
                    Id = 6,
                    ProductId = 4,
                    StoreId = 2
                }
            );
    }
}
