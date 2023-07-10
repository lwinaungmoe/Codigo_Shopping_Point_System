using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.BaseRepository;
using CodigoShopping.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoShopping.Infrastructure.DomainRepository
{
    public class ShoppingTransactionDetailsRepository : IShoppingTransactionDetailsRepository
    {
        private readonly CodigoShoppingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ShoppingTransactionDetailsRepository(CodigoShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<ShoppingTransactionDetails>> GetAllAsync()
        {
            return await _context.ShoppingTransactionDetails.ToListAsync();
        }

        public async Task<ShoppingTransactionDetails> GetById(int id)
        {
            return await _context.ShoppingTransactionDetails.FirstAsync(x => x.Id == id);
        }

        public async Task<ShoppingTransactionDetails> InsertAsync(ShoppingTransactionDetails entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();

            using (transaction)
            {
                await _context.ShoppingTransactionDetails.AddAsync(entity);

                await _context.CommitTransactionAsync(transaction);
            }

            return entity;
        }

        public async Task<ShoppingTransactionDetails> UpdateAsync(ShoppingTransactionDetails entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();
            using (transaction)
            {
                _context.Entry(entity).State = EntityState.Modified;

                await _context.CommitTransactionAsync(transaction);
            }

            ShoppingTransactionDetails catalogType = await _context.ShoppingTransactionDetails.FindAsync(entity.Id);

            return catalogType;
        }

        public Task<ShoppingTransactionDetails> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
