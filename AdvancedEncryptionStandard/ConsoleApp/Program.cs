using AdvancedEncryptionStandard;
using System;
using System.Text;

namespace ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var result = new Scrambler().WithKey("ABCDEFGHIJKLMNOP", 4).Encrypt("DESENVOLVIMENTO!A");

            Console.ReadLine();
        }
    }
}
