using Microsoft.AspNetCore.Mvc;
using Mobile.API.Model;
using Mobile.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mobile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
      

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
     
        // POST api/<AccountController>
        [HttpPost("Register")]
        public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {
            return await _appUserService.RegisterAppUser(registerRequest);
        }

        [HttpPost("RegisterConfirm")]
        public async Task<RegisterConfirmResponse> RegisterConfirm(RegisterConfimRequest registerConfimRequest)
        {
            return await _appUserService.ConfirmRegisterAppUser(registerConfimRequest);
        }

        [HttpPost("Signin")]
        public async Task<AppUserResponse> Signin(SignInRequest signInRequest)
        {
            return await _appUserService.SignIn(signInRequest);
        }



    }
}
