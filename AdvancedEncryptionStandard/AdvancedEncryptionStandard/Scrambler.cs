using AdvancedEncryptionStandard.Exceptions;
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
        private string Key { get; set; }
        private int KeySize { get; set; }
        private byte[] InitializationVector { get; set; }
        private CipherMode OperationMode { get; set; }
        private PaddingMode Padding { get; set; }
        private string[,] StateMatrix { get; set; }

        public Scrambler(string originalValue) : this(Encoding.ASCII.GetBytes(originalValue))
        {
        }

        public Scrambler(byte[] originalValue)
        {
            OriginalValue = originalValue;
            OperationMode = CipherMode.ECB;
            Padding = PaddingMode.PKCS7; // Mesmo algoritmo que PKCS#5
            KeySize = 128;

            StateMatrix = new string[4, 4];
        }

        public Scrambler WithKey(string key)
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
            ExpandirChave();

            return null;
        }

        private void ExpandirChave()
        {
            InicializarMatrizEstado(this.Key, this.KeySize);
        }

        private void InicializarMatrizEstado(string key, int keySize)
        {
            if (key.Length != keySize / 8)
                throw new InvalidKeyException("A chave \"{0}\" não condiz com o tamanho especificado ({1}).", key, keySize);

            for (int line = 0, index = 0; line < 4; line++)
                for (int column = 0; column < 4; column++, index++)
                    StateMatrix[column, line] = key[index].ToHexByte();
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

    public static class Extentions
    {
        public static string ToHexByte(this char value)
        {
            try
            {
                return Convert.ToByte(value).ToString("x2");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
