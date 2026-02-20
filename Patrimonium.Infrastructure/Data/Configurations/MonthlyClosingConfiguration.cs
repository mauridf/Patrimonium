using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Configurations
{
    public class MonthlyClosingConfiguration : IEntityTypeConfiguration<Patrimonium.Domain.Entities.MonthlyClosing>
    {
        public void Configure(EntityTypeBuilder<Patrimonium.Domain.Entities.MonthlyClosing> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Period, p =>
            {
                p.WithOwner();

                p.Property(x => x.Year)
                    .HasColumnName("year")
                    .HasColumnType("int")
                    .IsRequired();

                p.Property(x => x.Month)
                    .HasColumnName("month")
                    .HasColumnType("int")
                    .IsRequired();
            });

            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Version).IsRequired();
            builder.Property(x => x.GeneratedAt).IsRequired();
            builder.Property(x => x.IntegrityHash).IsRequired();

            // ==========================
            // ÍNDICES 
            // ==========================

            // Índice simples por usuário (performance)
            builder.HasIndex(x => new { x.UserId })
                   .HasDatabaseName("ix_monthly_closing_user");

            // Unicidade lógica por período (fechamento contábil real)
            builder.HasIndex("UserId", "year", "month")
                   .IsUnique()
                   .HasDatabaseName("ux_monthly_closing_user_period");
        }
    }
}
