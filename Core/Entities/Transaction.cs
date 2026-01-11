using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Entities
{
    public class Transaction : IEntity
    {
        [Key]
        public Guid TransactionId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CardHolderName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string MaskedCardNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = PaymentStatus.Authorized.ToString();

        [Required]
        [MaxLength(50)]
        public string OrderReference { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Property
        public ICollection<TransactionEvent>? TransactionEvents { get; set; }
    }
}
