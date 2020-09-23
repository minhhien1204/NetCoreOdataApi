using Microsoft.EntityFrameworkCore;
using NetCoreOdataApi.Core;
using NetCoreOdataApi.Core.Repositories;
using NetCoreOdataApi.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreOdataApi.Data
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        private readonly DataContext _context;
        protected DbTransaction Transaction;
        protected Dictionary<string, dynamic> Repositories;

        public UnitOfWork(IDataContext context)
        {
            _context = context as DataContext;
            Repositories = new Dictionary<string, dynamic>();
        }

        //public virtual IRepository<TEntity> Repository<TEntity>() where TEntity : class
        //{
        //    return RepositoryAsync<TEntity>();
        //}

        //public int? CommandTimeout
        //{
        //    get => _context.Database.CommandTimeout;
        //    set => _context.Database.CommandTimeout = value;
        //}

        public virtual int SaveChanges() => _context.SaveChanges();

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        //public virtual IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class
        //{
        //    if (Repositories == null)
        //    {
        //        Repositories = new Dictionary<string, dynamic>();
        //    }

        //    var type = typeof(TEntity).Name;

        //    if (Repositories.ContainsKey(type))
        //    {
        //        return (IRepositoryAsync<TEntity>)Repositories[type];
        //    }

        //    var repositoryType = typeof(Repository<>);

        //    Repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context, this));

        //    return Repositories[type];
        //}

        public IUnitOfWorkAsync NewUnitOfWorkAsync()
        {
            return new UnitOfWork(_context);
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql, cancellationToken, parameters);
        }

        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            //var objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            //if (objectContext.Connection.State != ConnectionState.Open)
            //{
            //    objectContext.Connection.Open();
            //}
            //Transaction = objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public virtual bool Commit()
        {
            _context.SaveChanges();
            return true;
        }

        public virtual void Rollback()
        {
            Transaction.Rollback();
            _context.Dispose();
        }
    }
}
