using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Mappings
{
    public class ContractMap : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("contracts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasOne<Property>()
                   .WithMany()
                   .HasForeignKey(x => x.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Person>()
                   .WithMany()
                   .HasForeignKey(x => x.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
