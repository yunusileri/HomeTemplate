using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;

namespace Sys
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly MyDbContext _context;

        public IQueryable<T> Table => throw new NotImplementedException();

        public IQueryable<T> TableNoTracking => throw new NotImplementedException();

        public Repository(MyDbContext ctx)
        {
            _context = ctx;
        }

        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Save();
        }

        public async Task Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Save();

        }

        public  IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }


        public T Get(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}