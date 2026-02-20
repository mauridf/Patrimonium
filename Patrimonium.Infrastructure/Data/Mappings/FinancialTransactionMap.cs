using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Mappings
{
    public class FinancialTransactionMap : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.ToTable("financial_transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Category).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.TransactionDate).IsRequired();
            builder.Property(x => x.CompetenceMonth).IsRequired();
            builder.Property(x => x.IsPaid).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne<Property>()
                   .WithMany()
                   .HasForeignKey(x => x.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Contract>()
                   .WithMany()
                   .HasForeignKey(x => x.ContractId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
