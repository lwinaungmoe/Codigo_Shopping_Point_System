using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.DomainRepository;

namespace Mobile.API.Services
{
    public class CatelogItemsService : ICatelogItemsService
    {
        private readonly ICatelogItemsRepository _catelogItemsRepository;

        public CatelogItemsService(ICatelogItemsRepository catelogItemsRepository)
        {
            _catelogItemsRepository = catelogItemsRepository;
        }

        public async Task<CatalogItem> DeletedGetCatelogItems(int id)
        {
            return await _catelogItemsRepository.DeleteAsync(id);
        }

        public async Task<List<CatalogItem>> GetAllGetCatelogItems()
        {
            return await _catelogItemsRepository.GetAllAsync();
        }

        public async Task<CatalogItem> GetCatelogItemsById(int id)
        {
            return await _catelogItemsRepository.GetById(id);
        }

        public async Task<CatalogItem> InsertGetCatelogItems(CatalogItem catalogType)
        {
            return await _catelogItemsRepository.InsertAsync(catalogType);
        }

        public async Task<CatalogItem> UpdateGetCatelogItems(CatalogItem catalogType)
        {
            return await _catelogItemsRepository.UpdateAsync(catalogType);
        }
    }
}