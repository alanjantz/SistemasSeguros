using System;
using AdvancedEncryptionStandard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AESTests
{
    [TestClass]
    public class ScramblerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var scrambler = new Scrambler("algo aqui nao sei me perdoa")
                                         .WithKey("ABCDEFGHIJKLMNOP");

            var criptografado = scrambler.Encrypt();
        }
    }
}
