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
    public class CatelogItemsRepository : ICatelogItemsRepository
    {
        private readonly CodigoShoppingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public CatelogItemsRepository(CodigoShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<CatalogItem> DeleteAsync(int id)
        {
            CatalogItem catalogItem = await _context.CatalogItems.FindAsync(id);

            _context.CatalogItems.Remove(catalogItem);

            await _context.SaveChangesAsync();

            return catalogItem;
        }

        public async Task<List<CatalogItem>> GetAllAsync()
        {
            return await _context.CatalogItems.ToListAsync();
        }

        public async Task<CatalogItem> GetById(int id)
        {
            return await _context.CatalogItems.FindAsync(id);
        }

        public async Task<CatalogItem> InsertAsync(CatalogItem entity)
        {
            await _context.CatalogItems.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<CatalogItem> UpdateAsync(CatalogItem entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            CatalogItem catalogItem = await _context.CatalogItems.FindAsync(entity.Id);

            return catalogItem;
        }
    }
}
