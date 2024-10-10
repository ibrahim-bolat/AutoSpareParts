using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class BrandSeriesRepository: Repository<BrandSeries>, IBrandSeriesRepository
{
    public BrandSeriesRepository(DataContext dbContext):base(dbContext)
    {
    }
}