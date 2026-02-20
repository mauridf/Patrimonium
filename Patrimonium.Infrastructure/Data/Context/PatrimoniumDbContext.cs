using Microsoft.EntityFrameworkCore;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Infrastructure.Data.Context
{
    public class PatrimoniumDbContext : DbContext
    {
        public PatrimoniumDbContext(DbContextOptions<PatrimoniumDbContext> options)
            : base(options) { }

        // DbSets (vão crescer por módulo)
        public DbSet<User> Users => Set<User>();
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<FinancialTransaction> FinancialTransactions => Set<FinancialTransaction>();
        public DbSet<Maintenance> Maintenances => Set<Maintenance>();
        public DbSet<Inspection> Inspections => Set<Inspection>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Media> Media => Set<Media>();
        public DbSet<Patrimonium.Domain.Entities.MonthlyClosing> MonthClosings => Set<Patrimonium.Domain.Entities.MonthlyClosing>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatrimoniumDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
