using Mobile.API.Model;

namespace Mobile.API.Services
{
    public interface IValidatorService
    {
        void ValidateRegisterRequest(RegisterRequest registerRequest);

        void ValidateRegisterConfirmRequest(RegisterConfimRequest registerConfimRequest);

        void ValidateUpdateRequest(UpdateUserRequest updateUserRequest);
    }
}
