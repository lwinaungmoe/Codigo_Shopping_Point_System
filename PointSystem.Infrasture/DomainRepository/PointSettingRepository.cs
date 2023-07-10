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
    public class PointSettingRepository : IPointSettingRepository
    {

        private readonly CodigoShoppingDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PointSettingRepository(CodigoShoppingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<PointSetting> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PointSetting>> GetAllAsync()
        {
            return await _context.PointSetting.ToListAsync();
        }

        public async Task<PointSetting> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PointSetting> InsertAsync(PointSetting entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();

            using (transaction)
            {
                await _context.PointSetting.AddAsync(entity);

                await _context.CommitTransactionAsync(transaction);
            }

            return entity;
        }

        public async Task<PointSetting> UpdateAsync(PointSetting entity)
        {
            await using var transaction = await _context.BeginTransactionAsync();
            using (transaction)
            {
                _context.Entry(entity).State = EntityState.Modified;

                await _context.CommitTransactionAsync(transaction);
            }

            PointSetting pointSetting = await _context.PointSetting.FindAsync(entity.Id);

            return pointSetting;
        }
    }
}
