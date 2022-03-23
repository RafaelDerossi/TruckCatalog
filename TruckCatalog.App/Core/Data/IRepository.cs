using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TruckCatalog.App.Core.DomainObjects;

namespace TruckCatalog.App.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
        IUnitOfWorks UnitOfWork { get; }

        Task<TEntity> GetById(Guid Id);               
        
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> expression, bool OrderByDesc = false, int take = 0);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(Func<TEntity, bool> predicate);        
    }
}