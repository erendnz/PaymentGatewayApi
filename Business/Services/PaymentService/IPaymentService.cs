using Business.DTOs.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PaymentService
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> AuthorizeTransactionAsync(PaymentRequestDto request);
        Task<PaymentResponseDto> GetTransactionByIdAsync(Guid id);
        Task<IEnumerable<PaymentResponseDto>> GetAllTransactionsAsync(int page, int pageSize);
        Task<bool> CaptureTransactionAsync(Guid id); 
        Task<bool> VoidTransactionAsync(Guid id);
        Task<IEnumerable<TransactionEventResponseDto>> GetTransactionEventsAsync(Guid transactionId);
    }
}
