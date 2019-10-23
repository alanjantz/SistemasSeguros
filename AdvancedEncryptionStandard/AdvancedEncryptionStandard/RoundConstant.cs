using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedEncryptionStandard
{
    public static class RoundConstant
    {
        private static byte[][] Table = new byte[11][]
            {
                new byte[] { 0x00, 0x00, 0x00, 0x00 },
                new byte[] { 0x01, 0x00, 0x00, 0x00 },
                new byte[] { 0x02, 0x00, 0x00, 0x00 },
                new byte[] { 0x04, 0x00, 0x00, 0x00 },
                new byte[] { 0x08, 0x00, 0x00, 0x00 },
                new byte[] { 0x10, 0x00, 0x00, 0x00 },
                new byte[] { 0x20, 0x00, 0x00, 0x00 },
                new byte[] { 0x40, 0x00, 0x00, 0x00 },
                new byte[] { 0x80, 0x00, 0x00, 0x00 },
                new byte[] { 0x1b, 0x00, 0x00, 0x00 },
                new byte[] { 0x36, 0x00, 0x00, 0x00 }
            };

        public static byte[] Get(int round)
        {
            return Table[round];
        }
    }
}
