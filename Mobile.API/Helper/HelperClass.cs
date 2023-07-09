using Microsoft.IdentityModel.Tokens;
using Mobile.API.Model;
using Mobile.API.ServiceCollectionExtensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Mobile.API.Helper
{
    public class HelperClass : IHelperClass
    {
        private const int NumberOfDigits = 6; // Number of digits in the OTP
        private const int TimeStepSeconds = 30; // Time step in seconds for TOTP

        public string PasswordHashing(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            return passwordHash;
        }

        public bool ValidateOTP(string otp, string orignalOtp, DateTime orignalExpired)
        {
            if (otp.Length != orignalOtp.Length)
            {
                return false;
            }
            else if (otp == orignalOtp)
            {
                if (orignalExpired > DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ValidatePassword(string password, string hashPassword)
        {
            bool verified = BCrypt.Net.BCrypt.Verify(password, hashPassword);

            return verified;
        }

        public string GenerateOTP()
        {
            string secretKeyHex = "ABCDEF1234567890"; // Replace with your actual secret key in hexadecimal format

            // Convert secret key from hexadecimal to byte array
            byte[] keyBytesda = Enumerable.Range(0, secretKeyHex.Length / 2)
                .Select(x => Convert.ToByte(secretKeyHex.Substring(x * 2, 2), 16))
                .ToArray();

            // Convert byte array to base32 string
            string secretKeyBase32 = Base32Encode(keyBytesda);

            byte[] keyBytes = Base32ToBytes(secretKeyBase32);

            long counter = GetCurrentCounter();

            using (var hmac = new HMACSHA1(keyBytes))
            {
                byte[] counterBytes = BitConverter.GetBytes(counter);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(counterBytes); // Ensure big-endian byte order
                }

                byte[] hash = hmac.ComputeHash(counterBytes);
                int offset = hash[hash.Length - 1] & 0x0F; // Get the offset
                int truncatedHash = ((hash[offset] & 0x7F) << 24) |
                                    ((hash[offset + 1] & 0xFF) << 16) |
                                    ((hash[offset + 2] & 0xFF) << 8) |
                                    (hash[offset + 3] & 0xFF);

                int otp = truncatedHash % (int)Math.Pow(10, NumberOfDigits);
                return otp.ToString().PadLeft(NumberOfDigits, '0');
            }
        }

        private static string Base32Encode(byte[] bytes)
        {
            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            int bits = 0, buffer = 0, count = 0;
            StringBuilder result = new StringBuilder();

            foreach (byte b in bytes)
            {
                buffer = (buffer << 8) | b;
                bits += 8;

                while (bits >= 5)
                {
                    int index = buffer >> (bits - 5);
                    result.Append(base32Chars[index]);
                    buffer &= (1 << (bits - 5)) - 1;
                    bits -= 5;
                    count++;
                }
            }

            if (bits > 0)
            {
                buffer <<= (5 - bits);
                int index = buffer;
                result.Append(base32Chars[index]);
                count++;
            }

            // Pad with '=' characters as needed
            int paddingCount = (count % 8 == 0) ? 0 : (8 - (count % 8));
            result.Append('=', paddingCount);

            return result.ToString();
        }
        public bool VerifyOTP(string secretKey, string enteredOTP, TimeSpan validTimeSpan)
        {
           
            string generatedOTP = GenerateOTP();

            // Compare the generated OTP with the entered OTP
            if (generatedOTP == enteredOTP)
            {
                // Check if the OTP is within the valid time span
                DateTime otpTime = GetTimeFromCounter(GetCurrentCounter());
                DateTime currentTime = DateTime.UtcNow;
                TimeSpan timeDifference = currentTime - otpTime;

                return timeDifference <= validTimeSpan;
            }

            return false;
        }

        private byte[] Base32ToBytes(string base32)
        {
            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            base32 = base32.TrimEnd('='); // Remove padding characters

            byte[] bytes = new byte[base32.Length * 5 / 8];
            int buffer = 0, next = 0, bitsRemaining = 0;

            foreach (char c in base32)
            {
                int value = base32Chars.IndexOf(c);
                if (value < 0)
                    throw new ArgumentException("Invalid base32 string.");

                buffer <<= 5;
                buffer |= value & 31;
                bitsRemaining += 5;

                if (bitsRemaining >= 8)
                {
                    bytes[next++] = (byte)(buffer >> (bitsRemaining - 8));
                    bitsRemaining -= 8;
                }
            }

            return bytes;
        }

        private long GetCurrentCounter()
        {
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1);
            long counter = (long)timeSpan.TotalSeconds / TimeStepSeconds;
            return counter;
        }

        private DateTime GetTimeFromCounter(long counter)
        {
            return new DateTime(1970, 1, 1).AddSeconds(counter * TimeStepSeconds);
        }

        public JWTTokenResponse GetJWTToken()
        {
            var tokenExpiredDate = DateTime.Now.AddMinutes(6);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManagerSetting.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: ConfigurationManagerSetting.AppSetting["JWT:ValidIssuer"],
                audience: ConfigurationManagerSetting.AppSetting["JWT:ValidAudience"],
                claims: new List<Claim>(),
                expires: tokenExpiredDate,
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return new JWTTokenResponse()
            {
                token = tokenString,
                tokenExpired = tokenExpiredDate
            };
        }
    }
}