using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities.NotDerived;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class NeighborhoodOrVillageRepository : Repository<NeighborhoodOrVillage>, INeighborhoodOrVillageRepository
{
    public NeighborhoodOrVillageRepository(DataContext dbContext):base(dbContext)
    {
    }
}