using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedEncryptionStandard
{
    public interface ICipher
    {
        byte[] Encrypt();
    }
}
