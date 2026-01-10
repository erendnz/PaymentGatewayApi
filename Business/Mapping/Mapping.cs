using AutoMapper;
using Business.DTOs.PaymentDTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Transaction = Entities.Transaction;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Transaction, PaymentResponseDto>();
            CreateMap<TransactionEvent, TransactionEventResponseDto>();
        }
    }
}
