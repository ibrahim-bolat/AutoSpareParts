using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;


namespace AutoSpareParts.Persistence.Repositories;

public class OrderDetailRepository: Repository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(DataContext dbContext):base(dbContext)
    {
    }
}