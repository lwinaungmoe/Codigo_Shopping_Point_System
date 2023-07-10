using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.DomainRepository;
using PointSystem.API.Model;

namespace PointSystem.API.Services
{
    public class CalculatePointSystemService : ICalcuatePointSystemService
    {
        private readonly IShoppingTransactionRepository _shoppingTransaction;

        private readonly IPointDataRepository _pointDataRepository;

        public CalculatePointSystemService(IShoppingTransactionRepository shoppingTransaction,

            IPointDataRepository pointDataRepository

           )

        {
            _shoppingTransaction = shoppingTransaction;

            _pointDataRepository = pointDataRepository;
        }

        public async Task<CalculateSystemResponse> CalculatePointSystemAsync()
        {
            CalculateSystemResponse calculateSystemResponse = new CalculateSystemResponse();
            try
            {
                var transactionList = await _shoppingTransaction.GetAllAsync();

                foreach (var transaction in transactionList)
                {
                    if (!transaction.IsCalculatePoint)
                    {
                        var pointData = await _pointDataRepository.GetById(transaction.AppUserId);
                        if (pointData != null)
                        {
                            pointData.NumberofPoint = transaction.TransactionPoint;
                            if (pointData.NumberofPoint > 1000)
                            {
                                pointData.NumberPointofAmount = 50;
                            }
                            else
                            {
                                pointData.NumberPointofAmount = 20;
                            }

                            await _pointDataRepository.UpdateAsync(pointData);
                        }
                        else
                        {
                            PointData pointData1 = new PointData()
                            {
                                NumberPointofAmount = 20,
                                NumberofPoint = transaction.TransactionPoint,
                                AppUserId = transaction.AppUserId,
                                Id= transaction.AppUserId
                            };

                            await _pointDataRepository.InsertAsync(pointData1);

                        }

                        transaction.IsCalculatePoint = true;

                        await _shoppingTransaction.UpdateAsync(transaction);
                    }
                }

                calculateSystemResponse.ErrorCode = "000";
                calculateSystemResponse.Message = "Success";

            }
            catch ( Exception ex )
            {
                calculateSystemResponse.ErrorCode = "009";
                calculateSystemResponse.Message = ex.Message;
            }
            return calculateSystemResponse;
        }
    }
}