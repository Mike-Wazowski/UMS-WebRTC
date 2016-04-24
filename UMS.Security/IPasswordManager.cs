using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Security
{
    public interface IPasswordManager
    {
        ISecurePassword EncryptPassword(string password);
        bool AreEqual(string password, string hashedPassword, string salt);
    }
}
