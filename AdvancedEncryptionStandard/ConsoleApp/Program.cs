﻿using AdvancedEncryptionStandard;
using System;

namespace ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var scrambler = new Scrambler("DESENVOLVIMENTO!").WithKey("ABCDEFGHIJKLMNOP", 4);

            scrambler.Encrypt();
            Console.ReadLine();
        }
    }
}
