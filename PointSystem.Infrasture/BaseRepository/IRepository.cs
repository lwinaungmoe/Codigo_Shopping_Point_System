using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoShopping.Infrastructure.BaseRepository
{
    public interface IRepository<T> where T : class
    {
     
        Task<List<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
}
