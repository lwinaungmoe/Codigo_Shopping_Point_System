using CodigoShopping.Domain.Model;
using Mobile.API.Model;

namespace Mobile.API
{
    public interface ICheckOutService
    {
        Task<CheckOutResponse> CheckOutAsync(CheckOutRequest request);

        Task<List<ShoppingTransaction>> GetShoppingTransactions();
    }
}
