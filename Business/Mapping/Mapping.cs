using AutoMapper;
using Business.DTOs.PaymentDTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
