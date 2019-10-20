using AdvancedEncryptionStandard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (var scrambler = new Scrambler("a"))
            {
                scrambler.WithKey("ABCDEFGHIJKLMNOP");

                scrambler.Encrypt();
            }
        }
    }
}
