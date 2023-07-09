namespace Mobile.API.Model
{
    public class AppUserResponse
    {
        public  string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int Id { get; set; } 
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string DeviceId { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public string OtpCode { get; set; }
        public DateTime OTPExpirayDateTIme { get; set; }
        public bool IsDeleted { get; set; }

        public JWTTokenResponse jWTTokenResponse { get; set; }
    }
}
