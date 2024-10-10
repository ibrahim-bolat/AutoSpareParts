using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class OemRepository : Repository<Oem>, IOemRepository
{
    public OemRepository(DataContext dbContext):base(dbContext)
    {
    }
}