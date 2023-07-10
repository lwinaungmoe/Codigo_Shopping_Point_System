using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.BaseRepository;
using CodigoShopping.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace CodigoShopping.Infrastructure.DomainRepository
{
    public class PointDataRepository : IPointDataRepository
    {
        private readonly CodigoShoppingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PointDataRepository(CodigoShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<PointData> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PointData>> GetAllAsync()
        {
            return await _context.PointData.ToListAsync();
        }

        public async Task<PointData> GetById(int id)
        {
            return await _context.PointData.FindAsync(id);
        }

        public async Task<PointData> InsertAsync(PointData entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();

            using (transaction)
            {
                await _context.PointData.AddAsync(entity);

                await _context.CommitTransactionAsync(transaction);
            }

            return entity;
        }

        public async Task<PointData> UpdateAsync(PointData entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();
            using (transaction)
            {
                _context.Entry(entity).State = EntityState.Modified;

                await _context.CommitTransactionAsync(transaction);
            }

            PointData pointData = await _context.PointData.FindAsync(entity.AppUserId);

            return pointData;
        }
    }
}