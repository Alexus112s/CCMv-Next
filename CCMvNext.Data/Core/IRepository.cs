using CCMvNext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CCMvNext.Data.Core
{
    /// <summary>
    /// Encapsulates a generic Repository for our app. Contains main CRUD methods.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        public Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);

        public Task<TEntity> CreateAsync(TEntity entity);

        public Task UpdateAsync(string id, TEntity entity);

        public Task RemoveAsync(string id);

        public IQueryable<TEntity> AsQueryable();
    }
}
