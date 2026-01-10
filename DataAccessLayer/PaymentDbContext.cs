using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DataAccessLayer
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext()
        {

        }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionEvent> TransactionEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions");
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.OrderReference)
                    .IsRequired()
                    .HasMaxLength(50);

                // Index addition for faster searching
                entity.HasIndex(e => e.OrderReference);
                entity.HasIndex(e => e.CreatedAt);
            });

            modelBuilder.Entity<TransactionEvent>(entity =>
            {
                entity.ToTable("TransactionEvents");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionEvents)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}