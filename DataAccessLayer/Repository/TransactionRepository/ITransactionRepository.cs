using Core.Entities;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.TransactionRepository
{
    public interface ITransactionRepository:IRepositoryBase<Transaction>
    {
    }
}
