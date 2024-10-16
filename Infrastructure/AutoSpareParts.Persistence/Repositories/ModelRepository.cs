using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class ModelRepository : Repository<Model>, IModelRepository
{
    public ModelRepository(DataContext dbContext):base(dbContext)
    {
    }
}