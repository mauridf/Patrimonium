using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Configurations
{
    public class MonthlyClosingConfiguration
    : IEntityTypeConfiguration<Patrimonium.Domain.Entities.MonthlyClosing>
    {
        public void Configure(EntityTypeBuilder<Patrimonium.Domain.Entities.MonthlyClosing> builder)
        {
            builder.ToTable("monthly_closings");
            builder.HasKey(x => x.Id);

            // ===== Core =====
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Version).IsRequired();
            builder.Property(x => x.GeneratedAt).IsRequired();
            builder.Property(x => x.IntegrityHash).IsRequired();

            builder.Property<int>("year").HasColumnName("year").IsRequired();
            builder.Property<int>("month").HasColumnName("month").IsRequired();

            // ===== Period (ValueObject) =====
            builder.OwnsOne(x => x.Period, p =>
            {
                p.Property(x => x.Year).HasColumnName("year").IsRequired();
                p.Property(x => x.Month).HasColumnName("month").IsRequired();
            });

            // ===== Indexes =====
            builder.HasIndex(x => x.UserId)
                   .HasDatabaseName("ix_monthly_closing_user");

            builder.HasIndex("UserId", "year", "month")
                   .IsUnique()
                   .HasDatabaseName("ux_monthly_closing_user_period");

            // ===== Snapshot =====
            builder.OwnsOne(x => x.Snapshot, snap =>
            {
                // Financial
                snap.OwnsOne(s => s.Financial, fin =>
                {
                    fin.Property(x => x.TotalIncome).HasColumnName("financial_total_income");
                    fin.Property(x => x.TotalExpense).HasColumnName("financial_total_expense");
                    fin.Property(x => x.TotalTax).HasColumnName("financial_total_tax");
                    fin.Property(x => x.TotalMaintenance).HasColumnName("financial_total_maintenance");
                    fin.Property(x => x.NetResult).HasColumnName("financial_net_result");
                    fin.Property(x => x.CashFlow).HasColumnName("financial_cash_flow");
                });

                // Operational
                snap.OwnsOne(s => s.Operational, op =>
                {
                    op.Property(x => x.ActiveProperties).HasColumnName("op_active_properties");
                    op.Property(x => x.VacantProperties).HasColumnName("op_vacant_properties");
                    op.Property(x => x.ActiveContracts).HasColumnName("op_active_contracts");
                    op.Property(x => x.OpenMaintenances).HasColumnName("op_open_maintenances");
                    op.Property(x => x.OccupancyRate).HasColumnName("op_occupancy_rate");
                    op.Property(x => x.VacancyRate).HasColumnName("op_vacancy_rate");
                });

                // Patrimonial
                snap.OwnsOne(s => s.Patrimonial, pat =>
                {
                    pat.Property(x => x.TotalEstimatedValue).HasColumnName("pat_total_estimated_value");
                    pat.Property(x => x.TotalInvested).HasColumnName("pat_total_invested");
                    pat.Property(x => x.Appreciation).HasColumnName("pat_appreciation");
                    pat.Property(x => x.RoiAccumulated).HasColumnName("pat_roi_accumulated");
                });

                // ===== Collections =====
                snap.OwnsMany(s => s.Transactions, tx =>
                {
                    tx.ToTable("monthly_snapshot_transactions");
                    tx.WithOwner().HasForeignKey("monthly_closing_id");
                    tx.HasKey("Id");

                    tx.Property<Guid>("Id");
                    tx.Property(x => x.TransactionId).HasColumnName("transaction_id");
                    tx.Property(x => x.Type).HasColumnName("type");
                    tx.Property(x => x.Amount).HasColumnName("amount");
                    tx.Property(x => x.Category).HasColumnName("category");
                    tx.Property(x => x.Date).HasColumnName("date");
                });

                snap.OwnsMany(s => s.Properties, prop =>
                {
                    prop.ToTable("monthly_snapshot_properties");
                    prop.WithOwner().HasForeignKey("monthly_closing_id");
                    prop.HasKey("Id");

                    prop.Property<Guid>("Id");
                    prop.Property(x => x.PropertyId).HasColumnName("property_id");
                    prop.Property(x => x.InternalName).HasColumnName("property_internal_name");
                    prop.Property(x => x.Type).HasColumnName("property_type");
                    prop.Property(x => x.Purpose).HasColumnName("property_purpose");
                    prop.Property(x => x.Income).HasColumnName("property_income");
                    prop.Property(x => x.Expense).HasColumnName("property_expense");
                    prop.Property(x => x.NetResult).HasColumnName("property_net_result");
                });
            });
        }
    }
}
