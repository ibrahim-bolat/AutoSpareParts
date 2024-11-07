using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class CustomerImageRepository : Repository<CustomerImage>, ICustomerImageRepository
{
    public CustomerImageRepository(DataContext dbContext):base(dbContext)
    {
    }
}