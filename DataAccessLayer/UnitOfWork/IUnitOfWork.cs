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
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository Transactions { get; }
        ITransactionEventRepository TransactionEvents { get; }
        Task<int> CommitAsync();
    }
}
