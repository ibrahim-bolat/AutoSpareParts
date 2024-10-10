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


        //identity 
        serviceCollection.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                    "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddEntityFrameworkStores<DataContext>()
            .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

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
        serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
        serviceCollection.AddScoped<IBrandRepository, BrandRepository>();
        serviceCollection.AddScoped<IBrandSeriesRepository, BrandSeriesRepository>();
        serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddScoped<ICityRepository, CityRepository>();
        serviceCollection.AddScoped<ICommentRepository, CommentRepository>();
        serviceCollection.AddScoped<IDistrictRepository, DistrictRepository>();
        serviceCollection.AddScoped<IEndpointRepository, EndpointRepository>();
        serviceCollection.AddScoped<IIpAddressRepository, IpAddressRepository>();
        serviceCollection.AddScoped<IMainCategoryRepository, MainCategoryRepository>();
        serviceCollection.AddScoped<IModelRepository, ModelRepository>();
        serviceCollection.AddScoped<INeighborhoodOrVillageRepository, NeighborhoodOrVillageRepository>();
        serviceCollection.AddScoped<IOemRepository, OemRepository>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<IProductImageRepository, ProductImageRepository>();
        serviceCollection.AddScoped<IRequestInfoLogRepository, RequestInfoLogRepository>();
        serviceCollection.AddScoped<IStreetRepository, StreetRepository>();
        serviceCollection.AddScoped<IUserImageRepository, UserImageRepository>();
        serviceCollection.AddScoped<IVehicleAddressRepository, VehicleAddressRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}