using CodigoShopping.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoShopping.Domain.Model
{
    public class AppUser :BaseEntity
    {
     
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string MobileNumber { get; set; }

        public bool IsConfirmMobileNumber { get; set; } 
        
        public string UserName { get; set; }

        public string DeviceId { get; set; }

        public DateTime? LastLoginDateTime { get; set; }

        public DateTime RegisterDateTime { get; set; }

        public string OtpCode { get; set; }

        public DateTime OTPExpirayDateTIme { get; set; }
        

      
    }
}
