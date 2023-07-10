using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.DomainRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Mobile.API.Caching;

namespace Mobile.API.Services
{
    public class PointSystemService : IPointSystemService
    {
        private readonly IPointDataRepository _pointDataRepository;
        private readonly ICacheService _cacheService;
        public PointSystemService(

            IPointDataRepository pointDataRepository
            , ICacheService cacheService

           )

        {
            _pointDataRepository = pointDataRepository;
            _cacheService = cacheService;
        }

        public async Task<PointData> GetPointData(int appUserId)
        {
            PointData pointData=new PointData();

            pointData = _cacheService.GetData<PointData>("PointData");
            if (pointData == null)
            {
                pointData = await _pointDataRepository.GetById(appUserId);

                if(pointData != null) {

                    var expirationTime = DateTimeOffset.Now.AddMinutes(3.0);
                    _cacheService.SetData<PointData>("PointData", pointData, expirationTime);

                    
                }
                
            }

            return pointData;

        }
    }
}