using Mobile.API.Model;

namespace Mobile.API
{
    public interface ICheckOutService
    {
        Task<CheckOutResponse> CheckOutAsync(CheckOutRequest request);
    }
}
