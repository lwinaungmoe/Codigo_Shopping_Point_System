using PointSystem.API.Model;

namespace PointSystem.API.Services
{
    public interface ICalcuatePointSystemService
    {
        Task<CalculateSystemResponse> CalculatePointSystemAsync();
    }
}
