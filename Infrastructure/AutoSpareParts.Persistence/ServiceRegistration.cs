using AutoSpareParts.Application.Constants;
using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories;
using AutoSpareParts.Persistence.Repositories.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Security.Principal;

namespace AutoSpareParts.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        //dbcontext
        serviceCollection.AddDbContext<DataContext>(options =>
        {
            //dotnet ef migrations add InitialCreate -s Presentation/AutoSpareParts.MVC -p Infrastructure/AutoSpareParts.Persistence
            //dotnet ef database update -s Presentation/AutoSpareParts.MVC -p Infrastructure/AutoSpareParts.Persistence
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                providerOptions => providerOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null));
        });


        //identity appuser
        serviceCollection.AddIdentity<AppUser, AppRole>(_ =>
        {
            configuration.GetSection("IdentityOptions");
        }).AddErrorDescriber<CustomIdentityErrorDescriber>()
           .AddEntityFrameworkStores<DataContext>()
           .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

        //identity employee
        serviceCollection.AddIdentityCore<Employee>(_ =>
        {
            configuration.GetSection("IdentityOptions");
        }).AddRoles<AppRole>()
          .AddErrorDescriber<CustomIdentityErrorDescriber>()
          .AddEntityFrameworkStores<DataContext>()
          .AddTokenProvider<DataProtectorTokenProvider<Employee>>(TokenOptions.DefaultProvider);

        //identity customer
        serviceCollection.AddIdentityCore<Customer>(_ =>
        {
            configuration.GetSection("IdentityOptions");
        }).AddRoles<AppRole>()
          .AddErrorDescriber<CustomIdentityErrorDescriber>()
          .AddEntityFrameworkStores<DataContext>()
          .AddTokenProvider<DataProtectorTokenProvider<Customer>>(TokenOptions.DefaultProvider);

        // Farklý kullanýcý türleri için altttaki servisleri ayrýca eklemek gerekiyor DI'a çünkü AddIdentity metodunda varsayýlan olarak eklendiði halde
        // AddIdentityCore da bunlar eklenmiyor.
        serviceCollection.TryAddScoped<SignInManager<Employee>>();
        serviceCollection.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<Employee>>();
        serviceCollection.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<Employee>>();

        serviceCollection.TryAddScoped<SignInManager<Customer>>();
        serviceCollection.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<Customer>>();
        serviceCollection.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<Customer>>();

        //user security stamp validate time
        serviceCollection.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.Zero;
        });

        // Sets the expiry to two hours
        serviceCollection.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(2);
        });


        //repositories
        //serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        serviceCollection.AddScoped<IAdRepository, AdRepository>();
        serviceCollection.AddScoped<IBrandRepository, BrandRepository>();
        serviceCollection.AddScoped<IBrandSeriesRepository, BrandSeriesRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<ICityRepository, CityRepository>();
        serviceCollection.AddScoped<ICommentRepository, CommentRepository>();
        serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
        serviceCollection.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
        serviceCollection.AddScoped<ICustomerImageRepository, CustomerImageRepository>();
        serviceCollection.AddScoped<IDiscountRepository, DiscountRepository>();
        serviceCollection.AddScoped<IDistrictRepository, DistrictRepository>();
        serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
        serviceCollection.AddScoped<IEmployeeAddressRepository, EmployeeAddressRepository>();
        serviceCollection.AddScoped<IEmployeeImageRepository, EmployeeImageRepository>();
        serviceCollection.AddScoped<IEndpointRepository, EndpointRepository>();
        serviceCollection.AddScoped<IInventoryRepository, InventoryRepository>();
        serviceCollection.AddScoped<IIPAddressRepository, IPAddressRepository>();
        serviceCollection.AddScoped<IMainCategoryRepository, MainCategoryRepository>();
        serviceCollection.AddScoped<IModelRepository, ModelRepository>();
        serviceCollection.AddScoped<INeighborhoodOrVillageRepository, NeighborhoodOrVillageRepository>();
        serviceCollection.AddScoped<IOemRepository, OemRepository>();
        serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
        serviceCollection.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        serviceCollection.AddScoped<IPaymentRepository, PaymentRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<IProductImageRepository, ProductImageRepository>();
        serviceCollection.AddScoped<IRequestInfoLogRepository, RequestInfoLogRepository>();
        serviceCollection.AddScoped<IStreetRepository, StreetRepository>();
        serviceCollection.AddScoped<ISupplierRepository, SupplierRepository>();
        serviceCollection.AddScoped<ISupplierAddressRepository, SupplierAddressRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}