using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Junto.Seguros.Domain.Commons
{
    public interface IRepositoryBase<T,TId> where T:DomainBase<TId>
    {
        T Get(TId id);
        T Get(Expression<Func<T, bool>> query);
        List<T> List(Expression<Func<T, bool>> query);

        IQueryable<T> AsQueryable();

       Task InsertAsync(T obj);

        Task UpdateAsync(T obj);

        Task DeleteAsync(T obj);

        Task CommitAsync();
    }

    public interface IRepositoryBase<T> : IRepositoryBase<T,long> where T : DomainBase
    {
        
    }
}
