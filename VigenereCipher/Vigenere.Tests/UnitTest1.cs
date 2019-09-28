using Vigenere;
using Xunit;

namespace Vigenere.Tests
{
    public class UnitTest1
    {
        const string decifrado = "They drink the tea";
        const string decifradoUpper = "THEYDRINKTHETEA";
        const string cifrado = "WBLBXYLHRWBLWYH";
        const string chave = "DUH";

        [Fact]
        public void Cifrar() => Assert.Equal(cifrado, Vigenere.Cifrar(chave, decifrado));

        [Fact]
        public void Decifrar() => Assert.Equal(decifradoUpper, Vigenere.Decifrar(chave, cifrado));
    }
}
