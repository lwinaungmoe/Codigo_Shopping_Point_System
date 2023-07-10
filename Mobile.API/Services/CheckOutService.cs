using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.DomainRepository;
using Mobile.API.Model;
using System.Runtime.CompilerServices;

namespace Mobile.API.Services
{
    public class CheckOutService : ICheckOutService
    {
        private readonly IShoppingTransactionRepository _shoppingTransaction;
        private readonly IShoppingTransactionDetailsRepository _shoppingTransactionDetailsRepository;
        private readonly IPointDataRepository _pointDataRepository;
        private readonly IPointSettingRepository _pointSettingRepository;
        private readonly ICatelogTypeRepository _catelogTypeRepository;

        public CheckOutService(IShoppingTransactionRepository shoppingTransaction,
            IShoppingTransactionDetailsRepository shoppingTransactionDetailsRepository,
            IPointDataRepository pointDataRepository,
            IPointSettingRepository pointSettingRepository,
            ICatelogTypeRepository catelogTypeRepository)

        {
            _shoppingTransaction = shoppingTransaction;
            _shoppingTransactionDetailsRepository = shoppingTransactionDetailsRepository;
            _pointDataRepository = pointDataRepository;
            _pointSettingRepository = pointSettingRepository;
            _catelogTypeRepository = catelogTypeRepository;
        }

        public async Task<CheckOutResponse> CheckOutAsync(CheckOutRequest request)
        {
            CheckOutResponse response = new CheckOutResponse();
            try
            {
                ShoppingTransaction shoppingTransaction = new ShoppingTransaction()
                {
                    AppUserId = request.AppUserId,
                    TotalTransactionAmount = CalcualteTotalAmount(request),
                    TransactionPoint = await GetTotalPoint(request),
                    TransactionDateTime = DateTime.UtcNow,
                    TransactionRefno = DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                    TransactionType = "PointSystem",
                    IsDeleted = false,
                };
                 
                var successInterData = await _shoppingTransaction.InsertAsync(shoppingTransaction);

                if (successInterData != null)
                {
                    foreach (var shoppingItem in request.Items)
                    {
                        ShoppingTransactionDetails shoppingTransactionDetails = new ShoppingTransactionDetails()
                        {
                            CatelogItemId = shoppingItem.Id,
                            TransactionId = successInterData.Id,
                            UnitPrice = shoppingItem.Price
                        };

                        await _shoppingTransactionDetailsRepository.InsertAsync(shoppingTransactionDetails);
                    }
                    response.ErrorCode = "000";
                    response.ErrorMessage = "Succsss";
                    response.TransactonId = successInterData.Id;
                    response.TranactionPoint = successInterData.TransactionPoint;
                    response.TranactionDateTime = successInterData.TransactionDateTime;
                    response.TransactionRefno = successInterData.TransactionRefno;
                    response.TotalAmount = request.TotalAmount;
                }
                else
                {
                    response.ErrorCode = "001";
                    response.ErrorMessage = "Fail";
                }
            }catch(ArgumentException ex)
            {
                response.ErrorCode = "002";
                response.ErrorMessage = "Amount Are MisMatch";
            }
            catch (Exception ex) {

                response.ErrorCode = "009";
                response.ErrorMessage = "System Error";
            }
           

            return response;
        }

        public async Task<List<ShoppingTransaction>> GetShoppingTransactions()
        {
            return await _shoppingTransaction.GetAllAsync();
        }

        private decimal CalcualteTotalAmount(CheckOutRequest checkOutRequest)
        {
            decimal totalAmount = 0M;
            try
            {

                foreach (var item in checkOutRequest.Items)
                {
                    totalAmount += item.Price;
                }
                if(totalAmount !=checkOutRequest.TotalAmount)
                {
                    throw new ArgumentException();
                }
                else
                {
                    return totalAmount;
                }
               

            }
            catch (Exception ex)
            {
                return totalAmount;
            }
            
        }
        private async Task<int> GetTotalPoint(CheckOutRequest checkOutRequest)
        {
            List<CatalogType> catalogType = await _catelogTypeRepository.GetAllAsync();

            List<PointSetting> pointSettings = await _pointSettingRepository.GetAllAsync();
            PointSetting pointSetting = new PointSetting();
            pointSetting = pointSettings.FirstOrDefault();
            CatalogType catalog = catalogType.Where(x => x.Type == "Alcohol").FirstOrDefault();
            int pointData = 0;
            if (catalog == null)
            {
                return 0;
            }
            else
            {
                foreach (var item in checkOutRequest.Items)
                {
                    if (catalog.Id != item.Id)
                    {
                        if (item.Price != 0.0M)
                        {
                            if (pointSetting.PointAmount <= item.Price)
                            {
                                pointData += pointSetting.PointMaxScore;
                            }
                        }
                    }
                }

                return pointData;
            }
        }
    }
}