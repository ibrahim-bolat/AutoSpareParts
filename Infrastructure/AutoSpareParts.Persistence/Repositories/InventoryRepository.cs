using System.Linq.Expressions;
using AutoSpareParts.Application.Repositories;
using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Persistence.Contexts;
using AutoSpareParts.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AutoSpareParts.Persistence.Repositories;

public class InventoryRepository: Repository<Inventory>, IInventoryRepository
{
    public InventoryRepository(DataContext dbContext):base(dbContext)
    {
    }
}