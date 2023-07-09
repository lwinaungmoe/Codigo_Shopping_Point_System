namespace Mobile.API.Model
{
    public class JWTTokenResponse
    {
        public string token { get; set; }
        public DateTime tokenExpired { get; set; }
    }
}
