using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class CategoryRepository: Repository<Category>, ICategoryRepository
{
    public CategoryRepository(DataContext dbContext):base(dbContext)
    {
    }
}