using Model.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetOneOrDefault(int id);
        TEntity GetOne(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(
            ISpecification<TEntity> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy=null,
            int limit=-1
        );

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int SaveChanges();
    }
}
