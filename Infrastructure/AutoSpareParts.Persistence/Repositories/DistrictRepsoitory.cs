using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class DistrictRepository : Repository<District>, IDistrictRepository
{
    public DistrictRepository(DataContext dbContext):base(dbContext)
    {
    }
}