using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Mappings
{
    public class PropertyMap : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("properties");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.InternalName).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Purpose).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.Country).IsRequired();
            builder.Property(x => x.ZipCode).IsRequired();

            builder.Property(x => x.CreatedAt).IsRequired();

            builder.OwnsOne(p => p.Valuation, v =>
            {
                v.Property(x => x.EstimatedValue)
                    .HasColumnName("estimated_value")
                    .IsRequired();

                v.Property(x => x.InvestedValue)
                    .HasColumnName("invested_value")
                    .IsRequired();
            });
        }
    }
}
