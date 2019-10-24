using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedEncryptionStandard
{
    public static class Extentions
    {
        public static string ToHexByte(this byte value, bool withPrefix = false)
        {
            try
            {
                return (withPrefix ? "0x" : string.Empty) + value.ToString("x2");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static byte[] ToArray(this byte[,] matrix)
        {
            List<byte> array = new List<byte>();
            for (int line = 0; line < matrix.Length; line++)
                for (int column = 0; column < 4; column++)
                    array.Add(matrix[line, column]);
            return array.ToArray();
        }
    }
}
