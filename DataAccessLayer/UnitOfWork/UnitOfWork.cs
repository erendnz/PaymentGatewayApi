using Core.Repository;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.TransactionEventRepository;
using DataAccessLayer.Repository.TransactionRepository;
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

        private TransactionRepository _transactionRepository; 
        private TransactionEventRepository _eventRepository; 

        public UnitOfWork(PaymentDbContext context) 
        { 
            _context = context; 
        } 

        // Lazy loading of repositories
        public ITransactionRepository Transactions => _transactionRepository ??= new TransactionRepository(_context); 
        public ITransactionEventRepository TransactionEvents => _eventRepository ??= new TransactionEventRepository(_context); 
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
