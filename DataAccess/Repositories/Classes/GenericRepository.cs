 using System.Linq.Expressions;
using DataAccess.Data.Contexts;
using DataAccess.Models.Shared;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) 
        : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
       
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _dbContext.Set<TEntity>().Where(e => e.IsDeleted != true).ToList();
            else
                return _dbContext.Set<TEntity>().Where(e => e.IsDeleted != true).AsNoTracking().ToList();
        }

        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity ,bool>> predigate)
        {
            return _dbContext.Set<TEntity>()
                             .Where(predigate).ToList();
        }


    }
}
