using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.BaseRepository;
using CodigoShopping.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace CodigoShopping.Infrastructure.DomainRepository
{
    public class ShoppingTransactionRepository : IShoppingTransactionRepository
    {
        private readonly CodigoShoppingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ShoppingTransactionRepository(CodigoShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ShoppingTransaction> DeleteAsync(int id)
        {
            ShoppingTransaction entity = await _context.ShoppingTransaction.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            else
            {
                await using var transaction = await _context.BeginTransactionAsync();
                using (transaction)
                {
                    entity.IsDeleted = true;
                    _context.Entry(entity).State = EntityState.Modified;

                    await _context.CommitTransactionAsync(transaction);
                }

                return entity;
            }
        }

        public async Task<List<ShoppingTransaction>> GetAllAsync()
        {
            return await _context.ShoppingTransaction.ToListAsync();
        }

        public async Task<ShoppingTransaction> GetById(int id)
        {
            return await _context.ShoppingTransaction.FindAsync(id);
        }

        public async Task<ShoppingTransaction> InsertAsync(ShoppingTransaction entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();

            using (transaction)
            {
                await _context.ShoppingTransaction.AddAsync(entity);

                await _context.CommitTransactionAsync(transaction);
            }

            return entity;
        }

        public async Task<ShoppingTransaction> UpdateAsync(ShoppingTransaction entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();
            using (transaction)
            {
                _context.Entry(entity).State = EntityState.Modified;

                await _context.CommitTransactionAsync(transaction);
            }

            ShoppingTransaction catalogType = await _context.ShoppingTransaction.FindAsync(entity.Id);

            return catalogType;
        }
    }
}