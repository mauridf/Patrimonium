using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Mappings
{
    public class DocumentMap : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("documents");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.FileUrl).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}