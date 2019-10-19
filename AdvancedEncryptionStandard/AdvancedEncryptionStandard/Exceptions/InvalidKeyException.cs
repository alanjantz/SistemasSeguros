using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedEncryptionStandard.Exceptions
{
    public class InvalidKeyException : Exception
    {
        public InvalidKeyException(string message, params object[] args)
        {
            throw new InvalidKeyException(string.Format(message, args));
        }

        public InvalidKeyException(string message) : base(message)
        {
        }
    }
}
