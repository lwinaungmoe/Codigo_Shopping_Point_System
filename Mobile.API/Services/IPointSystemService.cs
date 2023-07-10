using CodigoShopping.Domain.Model;

namespace Mobile.API.Services
{
    public interface IPointSystemService
    {
        Task<PointData> GetPointData(int appUserId);
    }
}
