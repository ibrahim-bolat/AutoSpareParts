namespace AutoSpareParts.Application.Repositories.Common;

public interface IUnitOfWork : IAsyncDisposable
{
    IAdRepository Ads { get; }
    IBrandRepository Brands { get; }
    IBrandSeriesRepository BrandSeries { get; }
    ICategoryRepository Categories { get; }
    ICityRepository Cities { get; }
    ICommentRepository Comments { get; }
    ICustomerRepository Customers { get; }
    ICustomerAddressRepository CustomerAddresses { get; }
    ICustomerImageRepository CustomerImages { get; }
    IDiscountRepository Discounts { get; }
    IDistrictRepository Districts { get; }
    IEmployeeRepository Employees { get; }
    IEmployeeAddressRepository EmployeeAddresses { get; }
    IEmployeeImageRepository EmployeeImages{ get; }
    IEndpointRepository Endpoints { get; }
    IInventoryRepository Inventories { get; }
    IIPAddressRepository IPAddresses { get; }
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
    ISupplierRepository Suppliers { get; }
    ISupplierAddressRepository SupplierAddresses { get; }
    Task<int> SaveAsync();
}