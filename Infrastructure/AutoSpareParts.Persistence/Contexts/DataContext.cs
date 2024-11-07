using System.Reflection;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Domain.Entities.NotDerivedFromBase;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Persistence.Contexts;

public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DbSet<Ad> Ads { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<BrandSeries> BrandSeries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<CustomerImage> Customermages { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    public DbSet<EmployeeImage> EmployeeImages { get; set; }
    public DbSet<Endpoint> Endpoints { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<IPAddress> IPAddresses { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<NeighborhoodOrVillage> NeighborhoodsOrVillages { get; set; }
    public DbSet<Oem> Oems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<RequestInfoLog> RequestInfoLogs { get; set; }
    public DbSet<Street> Streets { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierAddress> SupplierAddresses { get; set; }

    public DataContext(DbContextOptions<DataContext> dbContext) : base(dbContext)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.LogTo(Console.WriteLine);
}
