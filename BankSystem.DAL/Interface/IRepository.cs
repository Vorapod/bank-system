using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.DAL.Interface
{
    /// <summary>
    /// https://medium.com/falafel-software/implement-step-by-step-generic-repository-pattern-in-c-3422b6da43fd
    /// business logic is not aware whether the application is using LINQ to SQL or ADO.NET Entity Model ORM.
    /// In the future, underlying data sources or architecture can be changed without affecting the business logic.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity Add(TEntity entity);
        void Delete(TEntity Entity);
        void Delete(int id);
        void Update(TEntity Entity);
        TEntity GetById(int id);
    }
}
