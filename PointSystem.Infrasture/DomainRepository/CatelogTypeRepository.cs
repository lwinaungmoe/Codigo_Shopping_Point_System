using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.BaseRepository;
using CodigoShopping.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace CodigoShopping.Infrastructure.DomainRepository
{
    public class CatelogTypeRepository : ICatelogTypeRepository
    {
        private readonly CodigoShoppingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CatelogTypeRepository(CodigoShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CatalogType> DeleteAsync(int id)
        {
            CatalogType catalogType = await _context.CatalogTypes.FindAsync(id);

            _context.CatalogTypes.Remove(catalogType);

              await  _context.SaveChangesAsync();

            return catalogType;
        }

        public async Task<List<CatalogType>> GetAllAsync()
        {
            return await _context.CatalogTypes.ToListAsync();
        }

        public async Task<CatalogType> GetById(int id)
        {
            return await _context.CatalogTypes.FindAsync(id);
        }

        public async Task<CatalogType> UpdateAsync(CatalogType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            CatalogType catalogType = await _context.CatalogTypes.FindAsync(entity.Id);

            return catalogType;
        }

        public async Task<CatalogType> InsertAsync(CatalogType entity)
        {
           await _context.CatalogTypes.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}