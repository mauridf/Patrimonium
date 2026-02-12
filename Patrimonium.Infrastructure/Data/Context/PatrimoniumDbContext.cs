using System.Linq.Expressions;
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
        public DbSet<FinancialTransaction> FinancialTransactions => Set<FinancialTransaction>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<Maintenance> Maintenances => Set<Maintenance>();
        public DbSet<Inspection> Inspections => Set<Inspection>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Media> Media => Set<Media>();
        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<Alert> Alerts => Set<Alert>();
        public DbSet<SystemEvent> SystemEvents => Set<SystemEvent>();
        public DbSet<Billing> Billings => Set<Billing>();
        public DbSet<Vacancy> Vacancies => Set<Vacancy>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");

                    var prop = Expression.Property(parameter, "IsDeleted");
                    var condition = Expression.Equal(prop, Expression.Constant(false));

                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesTransactionalAsync()
        {
            using var transaction = await Database.BeginTransactionAsync();
            try
            {
                var result = await SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}
