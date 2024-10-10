using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Application.Repositories.Common;
using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AutoSpareParts.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;
    public IAdRepository Ads { get; }
    public IAddressRepository Addresses { get; }

    public IBrandRepository Brands { get; }

    public IBrandSeriesRepository BrandSeries { get; }

    public ICategoryRepository Categories { get; }

    public ICityRepository Cities { get; }

    public ICommentRepository Comments { get; }

    public IDistrictRepository Districts { get; }

    public IEndpointRepository Endpoints { get; }

    public IIpAddressRepository IpAddresses { get; }

    public IMainCategoryRepository MainCategories { get; }

    public IModelRepository Models { get; }

    public INeighborhoodOrVillageRepository NeighborhoodOrVillages { get; }

    public IOemRepository Oems { get; }

    public IProductRepository Products { get; }

    public IProductImageRepository ProductImages { get; }

    public IRequestInfoLogRepository RequestInfoLogs { get; }

    public IStreetRepository Streets { get; }

    public IUserImageRepository UserImages { get; }

    public IVehicleAddressRepository VehicleAddresses { get; }

    public UnitOfWork(DataContext dbContext, IAdRepository adRepository, IAddressRepository addressRepository, IBrandRepository brandRepository,
        IBrandSeriesRepository brandSeriesRepository, ICategoryRepository categoryRepository, ICityRepository cityRepository, ICommentRepository commentRepository, 
        IDistrictRepository districtRepository, IEndpointRepository endpointRepository, IIpAddressRepository ýpAddressRepository, IMainCategoryRepository mainCategoryRepository, 
        IModelRepository modelRepository, INeighborhoodOrVillageRepository neighborhoodOrVillageRepository, IOemRepository oemRepository, IProductRepository productRepository, 
        IProductImageRepository productImageRepository, IRequestInfoLogRepository requestInfoLogRepository, IStreetRepository streetRepository, IUserImageRepository userImageRepository, 
        IVehicleAddressRepository vehicleAddressRepository)
    {
        _dataContext = dbContext;
        Ads = adRepository;
        Addresses = addressRepository;
        Brands = brandRepository;
        BrandSeries = brandSeriesRepository;
        Categories = categoryRepository;
        Cities = cityRepository;
        Comments = commentRepository;
        Districts = districtRepository;
        Endpoints = endpointRepository;
        IpAddresses = ýpAddressRepository;
        MainCategories = mainCategoryRepository;
        Models = modelRepository;
        NeighborhoodOrVillages = neighborhoodOrVillageRepository;
        Oems = oemRepository;
        Products = productRepository;
        ProductImages = productImageRepository;
        RequestInfoLogs = requestInfoLogRepository;
        Streets = streetRepository;
        UserImages = userImageRepository;
        VehicleAddresses = vehicleAddressRepository;
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