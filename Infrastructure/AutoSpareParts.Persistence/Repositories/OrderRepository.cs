using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;


namespace AutoSpareParts.Persistence.Repositories;

public class OrderRepository: Repository<Order>, IOrderRepository
{
    public OrderRepository(DataContext dbContext):base(dbContext)
    {
    }
}