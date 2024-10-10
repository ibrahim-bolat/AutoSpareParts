using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class VehicleAddressRepository : Repository<VehicleAddress>, IVehicleAddressRepository
{
    public VehicleAddressRepository(DataContext dbContext):base(dbContext)
    {
    }
}