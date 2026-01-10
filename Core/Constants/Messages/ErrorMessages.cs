using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants.Messages
{
    public static class ErrorMessages
    {
        // API Error Messages
        public const string TransactionNotFound = "Transaction not found.";
        public const string InvalidCapture = "Only AUTHORIZED transactions can be captured.";
        public const string InvalidVoid = "Only AUTHORIZED transactions can be voided.";
        public const string ValidationError = "Validation failed.";
    }
}
