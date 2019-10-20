using AdvancedEncryptionStandard.Exceptions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedEncryptionStandard
{
    public class Scrambler : IDisposable, ICipher
    {
        private byte[] OriginalValue { get; set; }
        private byte[] EncryptedValue { get; set; }
        private string Key { get; set; }
        private int KeySize { get; set; }
        private byte[] InitializationVector { get; set; }
        private CipherMode OperationMode { get; set; }
        private PaddingMode Padding { get; set; }
        private byte[,] StateMatrix { get; set; }

        public Scrambler(string originalValue) : this(Encoding.ASCII.GetBytes(originalValue))
        {
        }

        public Scrambler(byte[] originalValue)
        {
            OriginalValue = originalValue;
            OperationMode = CipherMode.ECB;
            Padding = PaddingMode.PKCS7; // Mesmo algoritmo que PKCS#5
            KeySize = 128;

            StateMatrix = new byte[4, 4];
        }

        public Scrambler WithKey(string key)
        {
            this.Key = key;

            return this;
        }

        public Scrambler WithIV(byte[] iv)
        {
            this.InitializationVector = iv;

            return this;
        }

        public byte[] Encrypt()
        {
            ExpandKey();
            Round();

            return EncryptedValue;
        }

        private void Round()
        {
            InitializeRound();

            for (int round = 1; round < 10; round++)
                DoRound(round);

            FinalizeRound();
        }

        private void InitializeRound()
        {
            AddRoundKey(0);
        }

        private void DoRound(int round)
        {
            SubBytes();
            ShiftRows();
            MixClomuns();
            AddRoundKey(round);
        }

        private void FinalizeRound()
        {
            SubBytes();
            ShiftRows();
            AddRoundKey(10);
        }

        private void AddRoundKey(int round)
        {

        }

        private void SubBytes()
        {

        }

        private void ShiftRows()
        {

        }

        private void MixClomuns()
        {

        }

        private void ExpandKey()
        {
            if (Key.Length != KeySize / 8)
                throw new InvalidKeyException($"A chave \"{Key}\" não condiz com o tamanho especificado ({KeySize}).");

            InitializeStateMatrix(Key);
            WriteLog("Chave", StateMatrix);
        }

        private void InitializeStateMatrix(string key)
        {
            for (int column = 0, index = 0; column < 4; column++)
                for (int line = 0; line < 4; line++, index++)
                    StateMatrix[line, column] = (byte)key[index];
        }

        ~Scrambler()
        {
            Dispose();
        }

        public void Dispose()
        {
            OriginalValue = null;
        }

        public void WriteLog(string step, byte[,] matrix)
        {
            Console.WriteLine($"*** {step} ***");

            for (int line = 0; line < 4; line++)
                Console.WriteLine($"{matrix[line, 0].ToHexByte()} {matrix[line, 1].ToHexByte()} {matrix[line, 2].ToHexByte()} {matrix[line, 3].ToHexByte()}");
        }
    }

    public static class Extentions
    {
        public static string ToHexByte(this byte value)
        {
            try
            {
                return "0x" + value.ToString("x2");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
