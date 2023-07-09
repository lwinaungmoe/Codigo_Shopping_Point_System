using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.BaseRepository;
using CodigoShopping.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace CodigoShopping.Infrastructure.DBContexts
{
    public class CodigoShoppingDbContext : DbContext, IUnitOfWork
    {
        public CodigoShoppingDbContext(DbContextOptions<CodigoShoppingDbContext> options) : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionDetails> TransactionDetails { get; set; }

        public DbSet<PointData> PointData { get; set; }

        public DbSet<PointSetting> PointSetting { get; set; }

      //  private readonly IMediator _mediator;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        private IDbContextTransaction _currentTransaction;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
            builder.Entity<CatalogType>().ToTable(nameof(CatalogType));
            builder.Entity<AppUser>().ToTable(nameof(AppUsers));
            builder.Entity<Transaction>().ToTable(nameof(Transactions));
            builder.Entity<TransactionDetails>().ToTable(nameof(TransactionDetails));
            builder.Entity<PointData>().ToTable(nameof(PointData));
            builder.Entity<PointSetting>().ToTable(nameof(PointSetting));
        }

      

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

      





    }
}