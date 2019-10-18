using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedEncryptionStandard
{
    public class Scrambler : IDisposable, ICipher
    {
        private byte[] OriginalValue { get; set; }
        private byte[] Key { get; set; }
        private int KeySize { get; set; }
        private byte[] InitializationVector { get; set; }

        private CipherMode OperationMode { get; set; }

        public Scrambler(string originalValue) : this(Encoding.ASCII.GetBytes(originalValue))
        {
        }

        public Scrambler(byte[] originalValue)
        {
            OriginalValue = originalValue;
            OperationMode = CipherMode.ECB;
            KeySize = 128;
        }

        public Scrambler WithKey(string key)
        {
            return this.WithKey(Encoding.ASCII.GetBytes(key));
        }

        public Scrambler WithKey(byte[] key)
        {
            this.Key = key;

            return this;
        }

        public Scrambler WithIV(byte[] iv)
        {
            this.InitializationVector = iv;

            return this;
        }

        public byte[] Encrypt()
        {
            return null;
        }

        ~Scrambler()
        {
            Dispose();
        }

        public void Dispose()
        {
            OriginalValue = null;
        }
    }
}
