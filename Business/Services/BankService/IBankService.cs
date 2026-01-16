using Business.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.BankService
{
    public interface IBankService
    {
        Task<string> AuthorizeAsync(PaymentRequestDto request);
    }
}
