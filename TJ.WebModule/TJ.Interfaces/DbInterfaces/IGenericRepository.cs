using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TJ.Domain.Entities;

namespace TJ.Interfaces.DbInterfaces
{
    public interface IGenericRepository<TEntity>
     where TEntity : class, IEntity
    {
        Task<List<TEntity>> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           int first = 0, int offset = 0, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> Get(int id);

        Task<TEntity> Create(TEntity entity);
    }
}
