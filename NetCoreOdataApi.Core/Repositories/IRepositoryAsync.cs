﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreOdataApi.Core.Repositories
{
    public interface IRepositoryAsync<TEntity>:IRepository<TEntity> where TEntity:class
    {
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
