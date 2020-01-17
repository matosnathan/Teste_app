using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Junto.Seguros.Data.Context;
using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.Data.Commons
{
    public class RepositoryBase<T, TId> : IRepositoryBase<T, TId>
        where T : DomainBase<TId>
    {
        protected readonly DataContext _db;
        public RepositoryBase(DataContext db)
        {
            _db = db;
        }

        public T Get(TId id)
        {
            return _db.Set<T>().Find(id);
        }

        public T Get(Expression<Func<T, bool>> query)
        {
            return _db.Set<T>().FirstOrDefault(query);
        }

        public List<T> List(Expression<Func<T, bool>> query)
        {
            return _db.Set<T>().Where(query).ToList();
        }

        public IQueryable<T> AsQueryable()
        {
            return _db.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T obj)
        {
            await _db.Set<T>().AddAsync(obj);
        }

        public async Task UpdateAsync(T obj)
        {
            _db.Set<T>().Update(obj);
        }

        public async Task DeleteAsync(T obj)
        {
            _db.Set<T>().Remove(obj);
        }

        public async Task CommitAsync()
        {
            _db.SaveChanges();
        }
    }

    public class RepositoryBase<T> : RepositoryBase<T, long>
    where T : DomainBase<long>
    {
        public RepositoryBase(DataContext db) : base(db)
        {
        }
    }
}
