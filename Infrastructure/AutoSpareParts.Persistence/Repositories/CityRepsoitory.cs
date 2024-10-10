using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class CityRepository: Repository<City>, ICityRepository
{
    public CityRepository(DataContext dbContext):base(dbContext)
    {
    }
}