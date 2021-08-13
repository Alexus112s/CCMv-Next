using CCMvNext.Data.Core;
using CCMvNext.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCMvNext.Data.Extensions
{
    /// <summary>
    /// A set of various usefull <see cref="IRepository{TEntity}"/> extensions. 
    /// This to keep the actual repository codebase as narrow as possible.
    /// </summary>
    public static class RepositoryExtensions
    {
        public static async Task RemoveAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity)
            where TEntity : Entity
        {
            await repository.RemoveAsync(entity.Id);
        }

        public static async Task<TEntity> FindByIdAsync<TEntity>(this IRepository<TEntity> repository, string id)
            where TEntity : Entity
        {
            var res = await repository.FindAsync(x => x.Id == id);
            
            return res.FirstOrDefault();
        }

        public static async Task<IList<TEntity>> GetAllAsync<TEntity>(this IRepository<TEntity> repository)
            where TEntity : Entity
        {
            var res = await repository.FindAsync(_ => true);

            return res;
        }
    }
}
