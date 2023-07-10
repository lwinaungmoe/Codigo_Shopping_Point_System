using AutoMapper;
using CodigoShopping.Domain.Model;
using CodigoShopping.Infrastructure.DomainRepository;
using Mobile.API.Helper;
using Mobile.API.Model;

namespace Mobile.API.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IHelperClass _helperClass;
        private readonly string secretKey = "P@ssw0rd12345";
        private readonly TimeSpan validTimeSpan = TimeSpan.FromMinutes(5);

        public AppUserService(IAppUserRepository appUserRepository, IMapper mapper, IHelperClass helperClass)
        {
            _appUserRepository = appUserRepository;
            _helperClass = helperClass;
            _mapper = mapper;
        }

        public async Task<RegisterConfirmResponse> ConfirmRegisterAppUser(RegisterConfimRequest request)
        {
            bool isValidOTP = _helperClass.VerifyOTP(secretKey, request.Otp, validTimeSpan);
            if (!isValidOTP)
            {
                AppUser appUser = await _appUserRepository.GetById(request.Id);
                if (appUser == null)
                {
                    return new RegisterConfirmResponse()
                    {
                        ErrorCode = "001",
                        ErrorMessage = "Fail Validate OTP"
                    };
                }
                else
                {
                    isValidOTP = _helperClass.ValidateOTP(request.Otp, appUser.OtpCode, appUser.OTPExpirayDateTIme);
                    if (!isValidOTP)
                    {
                        return new RegisterConfirmResponse()
                        {
                            ErrorCode = "001",
                            ErrorMessage = "Fail Validate OTP"
                        };
                    }
                    else
                    {
                        appUser.IsConfirmMobileNumber = true;
                        await _appUserRepository.UpdateAsync(appUser);
                        var response = _mapper.Map<RegisterConfirmResponse>(appUser);

                        response.ErrorCode = "000";
                        response.ErrorMessage = "Success";
                        response.jWTTokenResponse = _helperClass.GetJWTToken();
                        return response;
                    }
                }
            }
            else
            {
                AppUser appUser = await _appUserRepository.GetById(request.Id);

                appUser.IsConfirmMobileNumber = true;
                await _appUserRepository.UpdateAsync(appUser);

                var response = _mapper.Map<RegisterConfirmResponse>(appUser);

                response.ErrorCode = "000";
                response.ErrorMessage = "Success";
                response.jWTTokenResponse = _helperClass.GetJWTToken();
                return response;
            }
        }

        public async Task<List<AppUserResponse>> GetAllAppUser()
        {
            throw new NotImplementedException();
        }

        public async Task<AppUserResponse> GetAppUserById()
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterResponse> RegisterAppUser(RegisterRequest request)
        {
            List<AppUser> appUsers = new List<AppUser>();

            appUsers = await _appUserRepository.GetAllAsync();

            AppUser appUser = appUsers.Where(x => x.MobileNumber == request.MobileNumber && x.IsConfirmMobileNumber == true).FirstOrDefault();

            if (appUser != null)
            {
                return new RegisterResponse()
                {
                    ErrorCode = "001",
                    ErrorMessage = "Can't register .Please user another mobile number."
                };
            }
            else
            {
                var requestCore = _mapper.Map<AppUser>(request);

                requestCore.PasswordHash = _helperClass.PasswordHashing(request.Password);

                requestCore.OtpCode = _helperClass.GenerateOTP();

                requestCore.RegisterDateTime = DateTime.UtcNow;

                requestCore.OTPExpirayDateTIme = DateTime.UtcNow.AddMinutes(2);

                AppUser appuser = await _appUserRepository.InsertAsync(requestCore);

                var response = _mapper.Map<RegisterResponse>(appuser);
                response.ErrorCode = "000";
                response.ErrorMessage = "Success";

                return response;
            }
        }

        public async Task<AppUserResponse> SignIn(SignInRequest signInRequest)
        {
            List<AppUser> appUsers = new List<AppUser>();

            appUsers = await _appUserRepository.GetAllAsync();

            AppUser appUser = appUsers.Where(x => x.MobileNumber == signInRequest.MobileNumber).FirstOrDefault();
            if (appUser == null)
            {
                return new AppUserResponse()
                {
                    ErrorCode = "001",
                    ErrorMessage = "Your mobile number have not been register.Please correct number"
                };
            }
            else
            {
                if (_helperClass.ValidatePassword(signInRequest.Password, appUser.PasswordHash))
                {
                    var resposne = _mapper.Map<AppUserResponse>(appUser);

                    resposne.ErrorCode = "000";

                    resposne.ErrorMessage = "Success";
                    resposne.jWTTokenResponse = _helperClass.GetJWTToken();
                    return resposne;
                }
                else
                {
                    return new AppUserResponse()
                    {
                        ErrorCode = "001",
                        ErrorMessage = "Invalid Password or Mobile Number."
                    };
                }
            }
        }

        public async Task<RegisterConfirmResponse> UpdateUser(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}