using AdvancedEncryptionStandard;
using System;
using System.Text;

namespace ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var scrambler = new Scrambler("DESENVOLVIMENTO!").WithKey("ABCDEFGHIJKLMNOP", 4);

            var result = scrambler.Encrypt();
            Console.WriteLine();

            Console.WriteLine(Encoding.Default.GetString(result));

            Console.ReadLine();
        }
    }
}
