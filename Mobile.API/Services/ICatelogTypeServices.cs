using CodigoShopping.Domain.Model;

namespace Mobile.API.Services
{
    public interface ICatelogTypeServices
    {
        Task<CatalogType> GetCatelogTypeById(int id);

        Task<List<CatalogType>> GetAllCatelogTypes();

        Task<CatalogType> InsertCatelogType(CatalogType catalogType);

        Task<CatalogType> UpdateCatelogType(CatalogType catalogType);

        Task<CatalogType> DeletedCatelogType(int id);

    }
}
