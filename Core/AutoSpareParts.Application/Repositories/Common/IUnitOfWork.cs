using AutoSpareParts.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Repositories.Common;

public interface IUnitOfWork : IAsyncDisposable
{
    IAdRepository Ads { get; }
    IAddressRepository Addresses { get; }
    IBrandRepository Brands { get; }
    IBrandSeriesRepository BrandSeries { get; }
    ICategoryRepository Categories { get; }
    ICityRepository Cities { get; }
    ICommentRepository Comments { get; }
    IDiscountRepository Discounts { get; }
    IDistrictRepository Districts { get; }
    IEndpointRepository Endpoints { get; }
    IInventoryRepository Inventories { get; }
    IIpAddressRepository IpAddresses { get; }
    IMainCategoryRepository MainCategories { get; }
    IModelRepository Models { get; }
    INeighborhoodOrVillageRepository NeighborhoodOrVillages { get; }
    IOemRepository Oems { get; }
    IOrderRepository Orders { get; }
    IOrderDetailRepository OrderDetails { get; }
    IPaymentRepository Payments { get; }
    IProductRepository Products { get; }
    IProductImageRepository ProductImages { get; }
    IRequestInfoLogRepository RequestInfoLogs { get; }
    IStreetRepository Streets { get; }
    IUserImageRepository UserImages { get; }
    IVehicleAddressRepository VehicleAddresses { get; }
    Task<int> SaveAsync();
}