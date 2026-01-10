using DataAccessLayer.Repository;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Transaction> Transactions { get; }
        IRepository<TransactionEvent> TransactionEvents { get; }
        Task<int> CommitAsync();
    }
}
