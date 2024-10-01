using AutoSpareParts.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoSpareParts.Application.Repositories;

public interface IUnitOfWork:IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity, new();
    Task<int> SaveAsync();
}