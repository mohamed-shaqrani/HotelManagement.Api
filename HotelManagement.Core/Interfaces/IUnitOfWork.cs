using HotelManagement.App.Core.Entities;

namespace HotelManagement.App.Core.Interfaces;
public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task<int> SaveChangesAsync();
}
