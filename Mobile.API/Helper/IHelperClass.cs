using Mobile.API.Model;

namespace Mobile.API.Helper
{
    public interface IHelperClass
    {
        public string PasswordHashing(string password);

        public bool ValidatePassword(string password, string hashPassword);

        public bool ValidateOTP(string otp, string orignalOtp, DateTime orignalExpired);

        string GenerateOTP();

        bool VerifyOTP(string secretKey, string enteredOTP, TimeSpan validTimeSpan);

        JWTTokenResponse GetJWTToken();
    }
}