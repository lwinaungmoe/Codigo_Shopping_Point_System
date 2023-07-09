using CodigoShopping.Domain.Model;

namespace Mobile.API.Services
{
    public interface ICatelogItemsService
    {
        Task<CatalogItem> GetCatelogItemsById(int id);

        Task<List<CatalogItem>> GetAllGetCatelogItems();

        Task<CatalogItem> InsertGetCatelogItems(CatalogItem catalogType);

        Task<CatalogItem> UpdateGetCatelogItems(CatalogItem catalogType);

        Task<CatalogItem> DeletedGetCatelogItems(int id);
    }
}
