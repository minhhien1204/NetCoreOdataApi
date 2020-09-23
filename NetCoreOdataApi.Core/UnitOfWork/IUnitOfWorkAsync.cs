using NetCoreOdataApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreOdataApi.Core.UnitOfWork
{
    public interface IUnitOfWorkAsync:IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IUnitOfWorkAsync NewUnitOfWorkAsync();
        //IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class;
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
    }
}
