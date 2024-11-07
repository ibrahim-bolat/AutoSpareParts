using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class EmployeeAddressRepository : Repository<EmployeeAddress>, IEmployeeAddressRepository
{
    public EmployeeAddressRepository(DataContext dbContext):base(dbContext)
    {

    }
}