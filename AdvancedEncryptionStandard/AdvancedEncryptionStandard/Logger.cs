using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedEncryptionStandard
{
    public static class Logger
    {
        public static void Write(string step, byte[,] matrix)
        {
            Console.WriteLine($"*** {step} ***");

            for (int line = 0; line < 4; line++)
                Console.WriteLine($"{matrix[line, 0].ToHexByte(true)} {matrix[line, 1].ToHexByte(true)} {matrix[line, 2].ToHexByte(true)} {matrix[line, 3].ToHexByte(true)}");

            Console.WriteLine();
        }
    }
}
