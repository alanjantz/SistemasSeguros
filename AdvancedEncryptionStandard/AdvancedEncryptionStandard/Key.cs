using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedEncryptionStandard
{
    public class Key
    {
        public string Value { get; private set; }
        public int Size { get; private set; }
        public int Bits { get; private set; }

        public Key(string value, int size)
        {
            this.Value = value;
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
        }
    }
}
