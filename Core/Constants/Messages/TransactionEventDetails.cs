using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants.Messages
{
    public static class TransactionEventDetails
    {
        public const string AuthorizationSuccess = "Transaction has been authorized.";
        public const string CaptureSuccess = "Transaction has been captured.";
        public const string VoidSuccess = "Transaction has been voided.";
    }
}
