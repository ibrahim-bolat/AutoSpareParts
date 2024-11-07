using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Persistence.Contexts;

namespace AutoSpareParts.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IAdRepository Ads { get; }
    public IBrandRepository Brands { get; }
    public IBrandSeriesRepository BrandSeries { get; }
    public ICategoryRepository Categories { get; }
    public ICityRepository Cities { get; }
    public ICommentRepository Comments { get; }
    public ICustomerRepository Customers { get; }
    public ICustomerAddressRepository CustomerAddresses { get; }
    public ICustomerImageRepository CustomerImages { get; }
    public IDiscountRepository Discounts { get; }
    public IDistrictRepository Districts { get; }
    public IEmployeeRepository Employees { get; }
    public IEmployeeAddressRepository EmployeeAddresses { get; }
    public IEmployeeImageRepository EmployeeImages { get; }
    public IEndpointRepository Endpoints { get; }
    public IInventoryRepository Inventories { get; }
    public IIPAddressRepository IPAddresses { get; }
    public IMainCategoryRepository MainCategories { get; }
    public IModelRepository Models { get; }
    public INeighborhoodOrVillageRepository NeighborhoodOrVillages { get; }
    public IOemRepository Oems { get; }
    public IOrderRepository Orders { get; }
    public IOrderDetailRepository OrderDetails { get; }
    public IPaymentRepository Payments { get; }
    public IProductRepository Products { get; }
    public IProductImageRepository ProductImages { get; }
    public IRequestInfoLogRepository RequestInfoLogs { get; }
    public IStreetRepository Streets { get; }
    public ISupplierRepository Suppliers { get; }
    public ISupplierAddressRepository SupplierAddresses { get; }

    public UnitOfWork(DataContext dataContext, IAdRepository ads, IBrandRepository brands, IBrandSeriesRepository brandSeries, ICategoryRepository categories, 
        ICityRepository cities, ICommentRepository comments, ICustomerRepository customers, ICustomerAddressRepository customerAddresses, ICustomerImageRepository customerImages, 
        IDiscountRepository discounts, IDistrictRepository districts, IEmployeeRepository employees, IEmployeeAddressRepository employeeAddresses, 
        IEmployeeImageRepository employeeImages, IEndpointRepository endpoints, IInventoryRepository ýnventories, IIPAddressRepository ýpAddresses, 
        IMainCategoryRepository mainCategories, IModelRepository models, INeighborhoodOrVillageRepository neighborhoodOrVillages, IOemRepository oems, 
        IOrderRepository orders, IOrderDetailRepository orderDetails, IPaymentRepository payments, IProductRepository products, IProductImageRepository productImages, 
        IRequestInfoLogRepository requestInfoLogs, IStreetRepository streets, ISupplierRepository suppliers, ISupplierAddressRepository supplierAddresses)
    {
        _dataContext = dataContext;
        Ads = ads;
        Brands = brands;
        BrandSeries = brandSeries;
        Categories = categories;
        Cities = cities;
        Comments = comments;
        Customers = customers;
        CustomerAddresses = customerAddresses;
        CustomerImages = customerImages;
        Discounts = discounts;
        Districts = districts;
        Employees = employees;
        EmployeeAddresses = employeeAddresses;
        EmployeeImages = employeeImages;
        Endpoints = endpoints;
        Inventories = ýnventories;
        IPAddresses = ýpAddresses;
        MainCategories = mainCategories;
        Models = models;
        NeighborhoodOrVillages = neighborhoodOrVillages;
        Oems = oems;
        Orders = orders;
        OrderDetails = orderDetails;
        Payments = payments;
        Products = products;
        ProductImages = productImages;
        RequestInfoLogs = requestInfoLogs;
        Streets = streets;
        Suppliers = suppliers;
        SupplierAddresses = supplierAddresses;
    }

    public async ValueTask DisposeAsync()
    {
        await _dataContext.DisposeAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await _dataContext.SaveChangesAsync();
    }
}