namespace Mobile.API.Model
{
    public class RegisterConfirmResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int id { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string UserName { get; set; }

        public string DeviceId { get; set; }

        public JWTTokenResponse jWTTokenResponse { get; set; }
    }
}
