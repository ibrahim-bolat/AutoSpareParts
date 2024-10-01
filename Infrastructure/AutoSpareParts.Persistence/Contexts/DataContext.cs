using System.Reflection;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Persistence.Mappings;
using AutoSpareParts.Persistence.Mappings.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Persistence.Contexts;

public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DbSet<Ad> Ads { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<BrandSeries> BrandSeries { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Endpoint> Endpoints { get; set; }
    public DbSet<IpAddress> IpAddresses { get; set; }
    public DbSet<MainCategory> MainCategories { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<NeighborhoodOrVillage> NeighborhoodsOrVillages { get; set; }
    public DbSet<Oem> Oems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<RequestInfoLog> RequestInfoLogs { get; set; }
    public DbSet<Street> Streets { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<VehicleAddress> VehicleAddresses { get; set; }

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
}
