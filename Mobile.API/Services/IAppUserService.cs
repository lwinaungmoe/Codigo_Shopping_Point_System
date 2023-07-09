using Mobile.API.Model;

namespace Mobile.API.Services
{
    public interface IAppUserService
    {
        Task<RegisterResponse> RegisterAppUser(RegisterRequest request);

        Task<RegisterConfirmResponse> ConfirmRegisterAppUser(RegisterConfimRequest request);

        Task<RegisterConfirmResponse> UpdateUser(UpdateUserRequest request);

        Task<AppUserResponse> SignIn(SignInRequest signInRequest);

        Task<List<AppUserResponse>> GetAllAppUser();

        Task<AppUserResponse> GetAppUserById();
    }
}
