using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Security.Implementations
{
    public class PasswordManager : IPasswordManager
    {
        public bool AreEqual(string password, string hashedPassword, string salt)
        {
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentOutOfRangeException(String.Format("Value cannot be null or white space. Parameter name: {0}", nameof(password)));
            if (String.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentOutOfRangeException(String.Format("Value cannot be null or white space. Parameter name: {0}", nameof(hashedPassword)));
            if (String.IsNullOrWhiteSpace(salt))
                throw new ArgumentOutOfRangeException(String.Format("Value cannot be null or white space. Parameter name: {0}", nameof(salt)));
            return hashedPassword == Encrypt(password, salt);
        }

        public ISecurePassword EncryptPassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentOutOfRangeException(String.Format("Value cannot be null or white space. Parameter name: {0}", nameof(password)));
            string salt = GenerateSalt();
            string encryptedPasswordWithSalt = Encrypt(password, salt);
            return new SecurePassword(encryptedPasswordWithSalt, salt);
        }
        
        private string GenerateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[64];
            rng.GetNonZeroBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        private string Encrypt(string value)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] raw = Encoding.Default.GetBytes(value);
                var result = sha256.ComputeHash(raw);
                StringBuilder sb = new StringBuilder();
                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private string Encrypt(string value, string salt)
        {
            string encryptedValue = Encrypt(value);
            string encryptedValueWithSalt = Encrypt(String.Concat(encryptedValue, salt));
            return encryptedValueWithSalt;
        }
    }
}
