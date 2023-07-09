using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.BaseRepository;
using CodigoShopping.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace CodigoShopping.Infrastructure.DomainRepository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly CodigoShoppingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public AppUserRepository(CodigoShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetById(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task<AppUser> InsertAsync(AppUser entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();
            using (transaction)
            {
                await _context.AppUsers.AddAsync(entity);

                await _context.CommitTransactionAsync(transaction);
            }

            return entity;
        }

        public async Task<AppUser> UpdateAsync(AppUser entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            AppUser catalogItem = await _context.AppUsers.FindAsync(entity.Id);

            return catalogItem;
        }

        public async Task<AppUser> DeleteAsync(int id)
        {
            AppUser appUser = await _context.AppUsers.FindAsync(id);

            _context.AppUsers.Remove(appUser);

            await _context.SaveChangesAsync();

            return appUser;
        }
    }
}