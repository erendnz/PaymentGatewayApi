using AutoMapper;
using Business.DTOs.PaymentDTOs;
using Core.Constants.Messages;
using Core.Enums;
using DataAccessLayer.UnitOfWork;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaymentResponseDto> AuthorizeTransactionAsync(PaymentRequestDto request)
        {
            // Mask the credit card number for security 
            string maskedCardNumber = $"{request.CardNumber.Substring(0, 6)}******{request.CardNumber.Substring(request.CardNumber.Length - 4)}";

            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Amount = request.Amount,
                Currency = request.Currency,
                CardHolderName = request.CardHolderName,
                MaskedCardNumber = maskedCardNumber,
                OrderReference = request.OrderReference,
                Status = PaymentStatus.Authorized.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            // Add transaction to tracking
            await _unitOfWork.Transactions.AddAsync(transaction);

            // Add initial history event
            await _unitOfWork.TransactionEvents.AddAsync(new TransactionEvent
            {
                TransactionId = transaction.TransactionId,
                Type = "STATUS_CHANGE",
                Status = PaymentStatus.Authorized.ToString(),
                Details = TransactionEventDetails.AuthorizationSuccess,
                CreatedAt = DateTime.UtcNow
            });

            // Commit all changes as a single atomic operation
            await _unitOfWork.CommitAsync();

            return _mapper.Map<PaymentResponseDto>(transaction);
        }

        public async Task<PaymentResponseDto> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);

            // If transaction is null, AutoMapper will also return null by default
            return _mapper.Map<PaymentResponseDto>(transaction);
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetAllTransactionsAsync(int page, int pageSize)
        {
            var transactions = await _unitOfWork.Transactions.GetAllAsync();

            var pagedTransactions = transactions
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return _mapper.Map<IEnumerable<PaymentResponseDto>>(pagedTransactions);
        }

        public async Task<bool> CaptureTransactionAsync(Guid id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);

            // Only AUTHORIZED transactions can be captured
            if (transaction == null || transaction.Status != PaymentStatus.Authorized.ToString())
                return false;

            transaction.Status = PaymentStatus.Captured.ToString();
            transaction.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Transactions.Update(transaction);

            // Log the status change
            await _unitOfWork.TransactionEvents.AddAsync(new TransactionEvent
            {
                TransactionId = transaction.TransactionId,
                Type = "STATUS_CHANGE",
                Status = PaymentStatus.Captured.ToString(),
                Details = TransactionEventDetails.CaptureSuccess,
                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> VoidTransactionAsync(Guid id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);

            // Only AUTHORIZED transactions can be voided
            if (transaction == null || transaction.Status != PaymentStatus.Authorized.ToString())
                return false;

            transaction.Status = PaymentStatus.Voided.ToString();
            transaction.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Transactions.Update(transaction);

            await _unitOfWork.TransactionEvents.AddAsync(new TransactionEvent
            {
                TransactionId = transaction.TransactionId,
                Type = "STATUS_CHANGE",
                Status = PaymentStatus.Voided.ToString(),
                Details = TransactionEventDetails.VoidSuccess,
                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<TransactionEventResponseDto>> GetTransactionEventsAsync(Guid transactionId)
        {
            var events = await _unitOfWork.TransactionEvents.FindAsync(e => e.TransactionId == transactionId);
            
            return _mapper.Map<IEnumerable<TransactionEventResponseDto>>(events.OrderBy(e => e.CreatedAt));
        }
    }
}
