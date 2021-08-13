using CCMvNext.Data.Configuration;
using CCMvNext.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CCMvNext.Data.Core
{
    /// <summary>
    /// A generic MongoDB implementation for the repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MongoRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly IMongoCollection<TEntity> _entities;

        public MongoRepository(CookieConsentDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _entities = database.GetCollection<TEntity>(settings.CollectionName);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _entities.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            var find = await _entities.FindAsync(filter);
            return await find.ToListAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _entities.DeleteOneAsync(entity => entity.Id == id);
        }

        public async Task UpdateAsync(string id, TEntity entity)
        {
            await _entities.ReplaceOneAsync(entity => entity.Id == id, entity);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _entities.AsQueryable();
        }
    }
}
