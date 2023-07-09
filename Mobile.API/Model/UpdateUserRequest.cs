namespace Mobile.API.Model
{
    public class UpdateUserRequest
    {
        public string Email { get; set; }
       
        public string MobileNumber { get; set; }

        public string UserName { get; set; }

        public string DeviceId { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

     
       
    }
}
