using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Mappings
{
    public class MediaMap : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("media");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Url).IsRequired();
            builder.Property(x => x.IsCover).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne<Property>()
                   .WithMany()
                   .HasForeignKey(x => x.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}