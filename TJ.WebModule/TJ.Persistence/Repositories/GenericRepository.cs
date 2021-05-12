using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TJ.Domain.Entities;
using TJ.Interfaces.DbInterfaces;

namespace TJ.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity

            {
            private readonly DevicesDbContext _context;
            internal DbSet<TEntity> dbSet;
            public GenericRepository(DevicesDbContext context)
            {
                _context = context;
                _context.ChangeTracker.LazyLoadingEnabled = true;
                this.dbSet = _context.Set<TEntity>();

            }
            public async Task<List<TEntity>> Get(
               Expression<Func<TEntity, bool>> filter = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
               int first = 0, int offset = 0, params Expression<Func<TEntity, object>>[] includeProperties)
            {
                IQueryable<TEntity> query = dbSet;

                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (offset > 0)
                {
                    query = query.Skip(offset);
                }
                if (first > 0)
                {
                    query = query.Take(first);
                }

                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty.GetPropertyAccess().Name);
                }



                return await query.ToListAsync();

            }
            public async Task<TEntity> Get(int id)
            {
                return await _context.Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
            }

            public async Task<TEntity> Create(TEntity entity)
            {
                try
                {
                    await _context.Set<TEntity>()
                        .AddAsync(entity);
                    await _context.SaveChangesAsync();

                    return entity;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }




    }
}

