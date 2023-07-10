using CodigoShopping.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile.API.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mobile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class CheckoutController : ControllerBase
    {
      
        private readonly ICheckOutService _checkOutService;

        public CheckoutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }

        [HttpPost("CheckOut")]
        public async Task<CheckOutResponse> CheckOut(CheckOutRequest  request)
        {
          return await _checkOutService.CheckOutAsync(request);
        }

        [HttpGet("CheckOutHistory")]
        public async Task<List<ShoppingTransaction>> CheckOutHistory()
        {
            return await _checkOutService.GetShoppingTransactions();
        }



    }
}
