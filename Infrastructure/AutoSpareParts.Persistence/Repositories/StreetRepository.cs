using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities.NotDerived;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class StreetRepository : Repository<Street>, IStreetRepository
{
    public StreetRepository(DataContext dbContext):base(dbContext)
    {
    }
}