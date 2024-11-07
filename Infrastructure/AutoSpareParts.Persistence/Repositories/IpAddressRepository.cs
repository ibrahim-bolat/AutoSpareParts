using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class IPAddressRepository : Repository<IPAddress>, IIPAddressRepository
{
    public IPAddressRepository(DataContext dbContext):base(dbContext)
    {
    }
}