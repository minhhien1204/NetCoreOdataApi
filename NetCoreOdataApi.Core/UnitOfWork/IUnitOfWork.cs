using NetCoreOdataApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NetCoreOdataApi.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        int ExecuteSqlCommand(string sql, params object[] parameters);
        //IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        //int? CommandTimeout { get; set; }
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}
