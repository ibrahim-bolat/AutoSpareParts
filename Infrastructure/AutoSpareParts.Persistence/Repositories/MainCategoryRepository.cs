using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class MainCategoryRepository : Repository<MainCategory>, IMainCategoryRepository
{
    public MainCategoryRepository(DataContext dbContext):base(dbContext)
    {
    }
}