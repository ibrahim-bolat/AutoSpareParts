using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class EmployeeImageRepository : Repository<EmployeeImage>, IEmployeeImageRepository
{
    public EmployeeImageRepository(DataContext dbContext):base(dbContext)
    {

    }
}