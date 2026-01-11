using Core.Interfaces;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity, new()
    {
        protected readonly PaymentDbContext _context;

        public RepositoryBase(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<List<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _context.Set<TEntity>().Where(predicate).ToListAsync();

    }
}
