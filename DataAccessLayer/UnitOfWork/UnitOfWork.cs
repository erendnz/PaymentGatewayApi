using DataAccessLayer.Repository;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentDbContext _context;
        private Repository<Transaction> _transactionRepository;
        private Repository<TransactionEvent> _eventRepository;

        public UnitOfWork(PaymentDbContext context)
        {
            _context = context;
        }

        // Lazy loading of repositories
        public IRepository<Transaction> Transactions =>
            _transactionRepository ??= new Repository<Transaction>(_context);

        public IRepository<TransactionEvent> TransactionEvents =>
            _eventRepository ??= new Repository<TransactionEvent>(_context);

        public async Task<int> CommitAsync()
        {
            // Saves all changes in a single transaction
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
