using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Patrimonium.Domain.Entities;
using Property = Patrimonium.Domain.Entities.Property;

namespace Patrimonium.Infrastructure.Data.Context
{
    public class PatrimoniumDbContext : DbContext
    {
        public PatrimoniumDbContext(DbContextOptions<PatrimoniumDbContext> options) : base(options) { }

        // DbSets (vão crescer conforme o domínio)
        public DbSet<User> Users => Set<User>();
        public DbSet<Property> Properties => Set<Property>();
    }
}
