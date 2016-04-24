using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Security
{
    public interface ISecurePassword
    {
        string HashedPassword { get; }
        string Salt { get; }
    }
}
