using AdvancedEncryptionStandard.Exceptions;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedEncryptionStandard
{
    public class Scrambler : IDisposable, ICipher
    {
        private byte[] OriginalValue { get; set; }
        private string EncryptedValue { get; set; }
        private string Key { get; set; }
        private int KeySize { get; set; }
        private byte[] InitializationVector { get; set; }
        private CipherMode OperationMode { get; set; }
        private PaddingMode Padding { get; set; }
        private string[,] StateMatrix { get; set; }

        public Scrambler(string originalValue) : this(Encoding.ASCII.GetBytes(originalValue))
        {
        }

        public Scrambler(byte[] originalValue)
        {
            OriginalValue = originalValue;
            OperationMode = CipherMode.ECB;
            Padding = PaddingMode.PKCS7; // Mesmo algoritmo que PKCS#5
            KeySize = 128;

            StateMatrix = new string[4, 4];
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

        public string Encrypt()
        {
            ExpandKey();
            Round();

            return EncryptedValue;
        }

        private void Round()
        {
            InitializeRound();

            for (int round = 0; round < 10; round++)
                DoRound();
            
            FinalizeRound();
        }

        private void InitializeRound()
        {
            AddRoundKey();
        }

        private void DoRound()
        {
            SubBytes();
            ShiftRows();
            MixClomuns();
            AddRoundKey();
        }

        private void FinalizeRound()
        {
            SubBytes();
            ShiftRows();
            AddRoundKey();
        }

        private void AddRoundKey()
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
            for (int line = 0, index = 0; line < 4; line++)
                for (int column = 0; column < 4; column++, index++)
                    StateMatrix[column, line] = key[index].ToHexByte();
        }

        ~Scrambler()
        {
            Dispose();
        }

        public void Dispose()
        {
            OriginalValue = null;
        }

        public void WriteLog(string step, string[,] matrix)
        {
            Debug.WriteLine($"*** {step} ***");

            for (int line = 0; line < 4; line++)
                Debug.WriteLine($"{matrix[line, 0]} {matrix[line, 1]} {matrix[line, 2]} {matrix[line, 3]}");
        }
    }

    public static class Extentions
    {
        public static string ToHexByte(this char value)
        {
            try
            {
                return Convert.ToByte(value).ToString("x2");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
