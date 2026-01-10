using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PaymentDbContext _context;

        public Repository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(Guid id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<List<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().Where(predicate).ToListAsync();

    }
}
