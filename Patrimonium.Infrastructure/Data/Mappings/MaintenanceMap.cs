using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Mappings
{
    public class MaintenanceMap : IEntityTypeConfiguration<Maintenance>
    {
        public void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.ToTable("maintenances");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CostEstimation).IsRequired();
            builder.Property(x => x.OpenedAt).IsRequired();

            builder.HasOne<Property>()
                   .WithMany()
                   .HasForeignKey(x => x.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Person>()
                   .WithMany()
                   .HasForeignKey(x => x.ResponsiblePersonId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}