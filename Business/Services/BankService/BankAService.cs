using Business.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.BankService
{
    public class BankAService : IBankService
    {
        public async Task<string> AuthorizeAsync(PaymentRequestDto request)
        {
            return await Task.FromResult("Bank A");
        }
    }
}
