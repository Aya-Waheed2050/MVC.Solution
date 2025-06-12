using System.Linq.Expressions;
using DataAccess.Models.Shared;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<TEntity>  where TEntity : BaseEntity
    {

        public IEnumerable<TEntity> GetAll(bool withTracking = false);
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predigate);
        public TEntity? GetById(int id);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);
        public void Add(TEntity entity);
    }
}
