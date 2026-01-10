using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TransactionEvent
    {
        [Key]
        public int TransactionEventId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; } = string.Empty; 

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty; 

        public string? Details { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public Guid TransactionId { get; set; }

        // Navigation Property
        [ForeignKey("TransactionId")]
        public Transaction? Transaction { get; set; }
    }
}
