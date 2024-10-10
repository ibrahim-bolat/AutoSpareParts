using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class RequestInfoLogRepository : Repository<RequestInfoLog>, IRequestInfoLogRepository
{
    public RequestInfoLogRepository(DataContext dbContext):base(dbContext)
    {
    }
}