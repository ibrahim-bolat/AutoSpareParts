using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class BrandRepository: Repository<Brand>, IBrandRepository
{
    public BrandRepository(DataContext dbContext):base(dbContext)
    {
    }
}