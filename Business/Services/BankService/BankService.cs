using Business.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.BankService
{
    public class BankService : IBankService
    {
        private readonly BankAService _bankAService;
        private readonly BankBService _bankBService;
        private readonly BankCService _bankCService;

        public BankService(BankAService bankAService, BankBService bankBService, BankCService bankCService)
        {
            _bankAService = bankAService;
            _bankBService = bankBService;
            _bankCService = bankCService;
        }

        public async Task<string> AuthorizeAsync(PaymentRequestDto request)
        {

            IBankService selectedBank = request.Amount < 100 ? _bankAService :
                               request.Amount < 300 ? _bankBService :
                               _bankCService;

            return await selectedBank.AuthorizeAsync(request);
        }
    }
}
