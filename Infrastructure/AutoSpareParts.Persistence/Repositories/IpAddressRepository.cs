using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class IpAddressRepository : Repository<IpAddress>, IIpAddressRepository
{
    public IpAddressRepository(DataContext dbContext):base(dbContext)
    {
    }
}