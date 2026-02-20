using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.MonthlyClosing.Mappings
{
    public class MonthlyClosingMap : IEntityTypeConfiguration<Patrimonium.Domain.Entities.MonthlyClosing>
    {
        public void Configure(EntityTypeBuilder<Patrimonium.Domain.Entities.MonthlyClosing> builder)
        {
            builder.ToTable("month_closings");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Period, p =>
            {
                p.Property(x => x.Year).HasColumnName("year");
                p.Property(x => x.Month).HasColumnName("month");
            });

            builder.Property(x => x.Status);
            builder.Property(x => x.Version);
            builder.Property(x => x.GeneratedAt);
            builder.Property(x => x.LockedAt);
            builder.Property(x => x.ReopenedAt);
            builder.Property(x => x.IntegrityHash);

            builder.HasIndex(x => new { x.UserId, x.Version });
            builder.HasIndex("year", "month", "UserId").IsUnique();
        }
    }
}
