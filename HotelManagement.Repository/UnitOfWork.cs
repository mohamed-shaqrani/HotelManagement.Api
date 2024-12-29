﻿using HotelManagement.App.Core.Entities;
using HotelManagement.App.Core.Interfaces;
using System.Collections;

namespace HotelManagement.App.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    private readonly Hashtable _repositories;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _repositories = new Hashtable();
    }
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repository = new Repository<TEntity>(_appDbContext);
            _repositories.Add(type, repository);
        }

        return (IRepository<TEntity>)_repositories[type];

    }

    public async Task<int> SaveChangesAsync()
    {
      return  await _appDbContext.SaveChangesAsync();
    }
}