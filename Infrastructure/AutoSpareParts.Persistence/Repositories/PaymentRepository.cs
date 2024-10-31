using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;

namespace AutoSpareParts.Persistence.Repositories;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(DataContext dbContext):base(dbContext)
    {
    }
}