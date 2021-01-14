using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BankSystem.DAL.Interface;

namespace BankSystem.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext _dbContext;
        private IDbSet<TEntity> _dbSet => _dbContext.Set<TEntity>();
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public TEntity GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
