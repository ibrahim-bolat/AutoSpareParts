using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class EndpointRepository : Repository<Endpoint>, IEndpointRepository
{
    public EndpointRepository(DataContext dbContext):base(dbContext)
    {
    }
}