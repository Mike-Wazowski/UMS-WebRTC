using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Security.Implementations
{
    internal class SecurePassword : ISecurePassword
    {
        private string _hashedPassword;

        public string HashedPassword
        {
            get { return _hashedPassword; }
            private set { _hashedPassword = value; }
        }

        private string _salt;

        public string Salt
        {
            get { return _salt; }
            private set { _salt = value; }
        }

        public SecurePassword(string hashedPassword, string salt)
        {
            HashedPassword = hashedPassword;
            Salt = salt;
        }
    }
}
