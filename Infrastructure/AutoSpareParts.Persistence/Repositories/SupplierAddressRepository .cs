using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class SupplierAddressRepository : Repository<SupplierAddress>, ISupplierAddressRepository
{
    public SupplierAddressRepository(DataContext dbContext):base(dbContext)
    {
    }
}