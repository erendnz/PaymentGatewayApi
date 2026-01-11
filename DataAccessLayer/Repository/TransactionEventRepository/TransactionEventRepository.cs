using Core.Entities;
using Core.Repository;
using DataAccessLayer.Repository.TransactionRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.TransactionEventRepository
{
    public class TransactionEventRepository : RepositoryBase<TransactionEvent>, ITransactionEventRepository
    {
        public TransactionEventRepository(PaymentDbContext context) : base(context)
        {
        }
    }
}
