using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.PaymentDTOs
{
    public record TransactionEventResponseDto(
        string Type,
        string Status,
        string Details,
        DateTime CreatedAt
    );
}
