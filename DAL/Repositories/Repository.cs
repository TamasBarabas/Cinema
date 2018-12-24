using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal readonly DbContext context;
        internal readonly DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetOneOrDefault(int id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity GetOne(int id)
        {
            TEntity entity = dbSet.Find(id);
            if (entity == null) throw new KeyNotFoundException($"{typeof(TEntity)} with id '{id}' cannot be found");
            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<TEntity> Find(
            ISpecification<TEntity> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int limit = -1
        ) {
            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in filter.Includes)
            {
                query = query.Include(includeProperty);
            }
            query.Load();
            
            if (filter != null)
            {
                query = query.Where(e => filter.IsSatisfiedBy(e));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if(limit >= 0)
            {
                query = query.Take(limit);
            }

            return query.AsEnumerable();
            
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }        

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
