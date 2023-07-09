namespace Mobile.API.Model
{
    public class RegisterConfimRequest
    {
        public int Id { get; set; } 
        public string Otp { get; set; }

        public DateTime OtpExpired { get; set; }
    }
}
