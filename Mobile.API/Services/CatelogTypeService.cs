using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.DomainRepository;

namespace Mobile.API.Services
{
    public class CatelogTypeService : ICatelogTypeServices
    {
        private readonly ICatelogTypeRepository _catelogTypeRepository;  

        public CatelogTypeService(ICatelogTypeRepository catelogTypeRepository) { 
            _catelogTypeRepository=catelogTypeRepository;
        }
        public async Task<CatalogType> DeletedCatelogType(int id)
        {
            return await _catelogTypeRepository.DeleteAsync(id);
        }

        public async Task<List<CatalogType>> GetAllCatelogTypes()
        {
            return await _catelogTypeRepository.GetAllAsync();
        }

        public async Task<CatalogType> GetCatelogTypeById(int id)
        {
            return await _catelogTypeRepository.GetById(id);
        }

        public async Task<CatalogType> InsertCatelogType(CatalogType catalogType)
        {
            return await _catelogTypeRepository.InsertAsync(catalogType);
        }

        public async Task<CatalogType> UpdateCatelogType(CatalogType catalogType)
        {
            return await _catelogTypeRepository.UpdateAsync(catalogType);
        }
    }
}
