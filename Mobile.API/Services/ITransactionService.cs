using CodigoShopping.Domain.Model;

namespace Mobile.API.Services
{
    public interface ITransactionService
    {
        Task<List<ShoppingTransaction>> GetShoppingTransactions();
    }
}
