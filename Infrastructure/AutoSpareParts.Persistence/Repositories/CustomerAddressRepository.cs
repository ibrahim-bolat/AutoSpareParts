using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
{
    public CustomerAddressRepository(DataContext dbContext):base(dbContext)
    {
    }
}