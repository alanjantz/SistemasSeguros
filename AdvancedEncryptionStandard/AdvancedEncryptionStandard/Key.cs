using AdvancedEncryptionStandard.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdvancedEncryptionStandard
{
    public class Key
    {
        public byte[] Value { get; private set; }
        public int Size { get; private set; }
        public int Bits { get; private set; }

        public Key(string value, int size)
        {
            this.Value = ConvertToByteArray(value);
            this.Size = size;
            switch (size)
            {
                case 4:
                    this.Bits = 128;
                    break;
                case 6:
                    this.Bits = 192;
                    break;
                case 8:
                    this.Bits = 256;
                    break;
                default:
                    break;
            }

            if (this.Value.Length != this.Bits / 8)
                throw new InvalidKeyException($"A chave \"{value}\" não condiz com o tamanho especificado ({size}).");
        }

        private byte[] ConvertToByteArray(string value)
        {
            if (Regex.IsMatch(value, "([0-9]+([,][0-9]+)*)"))
            {
                var values = value.Split(',');
                List<byte> bytes = new List<byte>();
             
                for (int i = 0; i < values.Length; i++)
                    bytes.Add(Convert.ToByte(values[i]));

                return bytes.ToArray();
            }
            else
                return Encoding.ASCII.GetBytes(value);
        }
    }
}
