using HotelManagement.Core.Entities;

namespace HotelManagement.Core.Interfaces;
public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
}
