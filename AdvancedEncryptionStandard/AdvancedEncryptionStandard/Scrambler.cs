using AdvancedEncryptionStandard.Exceptions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedEncryptionStandard
{
    public class Scrambler
    {
        private byte[] OriginalValue { get; set; }
        private byte[] EncryptedValue { get; set; }
        private Key Key { get; set; }
        private byte[] InitializationVector { get; set; }
        private CipherMode OperationMode { get; set; }
        private PaddingMode Padding { get; set; }
        private byte[,] StateMatrix { get; set; }
        private byte[,] KeySchedule { get; set; }
        private byte[][] RoundConstant { get; set; }

        public Scrambler(string originalValue) : this(Encoding.ASCII.GetBytes(originalValue))
        {
        }

        public Scrambler(byte[] originalValue)
        {
            OriginalValue = originalValue;
            OperationMode = CipherMode.ECB;
            Padding = PaddingMode.PKCS7; // Mesmo algoritmo que PKCS#5

            RoundConstant = new byte[11][]
            {
                new byte[] { 0x00, 0x00, 0x00, 0x00 },
                new byte[] { 0x01, 0x00, 0x00, 0x00 },
                new byte[] { 0x02, 0x00, 0x00, 0x00 },
                new byte[] { 0x04, 0x00, 0x00, 0x00 },
                new byte[] { 0x08, 0x00, 0x00, 0x00 },
                new byte[] { 0x10, 0x00, 0x00, 0x00 },
                new byte[] { 0x20, 0x00, 0x00, 0x00 },
                new byte[] { 0x40, 0x00, 0x00, 0x00 },
                new byte[] { 0x80, 0x00, 0x00, 0x00 },
                new byte[] { 0x1b, 0x00, 0x00, 0x00 },
                new byte[] { 0x36, 0x00, 0x00, 0x00 }
            };
        }

        public Scrambler WithKey(string key, int bytes)
        {
            this.Key = new Key(key, bytes);

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

        #region ExpandKeys
        private void ExpandKey()
        {
            if (Key.Value.Length != Key.Bits / 8)
                throw new InvalidKeyException($"A chave \"{Key}\" não condiz com o tamanho especificado ({Key.Size}).");

            InitializeStateMatrix(Key.Value);
            DoRoundKeys();
        }

        private void InitializeStateMatrix(string key)
        {
            StateMatrix = new byte[4, 4];
            for (int column = 0, index = 0; column < 4; column++)
                for (int line = 0; line < 4; line++, index++)
                    StateMatrix[line, column] = (byte)key[index];
            WriteLog("Chave", StateMatrix);
        }

        private void DoRoundKeys()
        {
            int qtdColunas = 4;
            int qtdLinhas = 4 * (10 + 1); // quantidade de rodadas + chave original

            this.KeySchedule = new byte[qtdLinhas, qtdColunas];

            ApplyRound(0, 0, StateMatrix);

            for (int round = 1; round <= 10; round++)
            {
                var currentMatrix = new byte[4, 4];
                DoFirstColumnRound(ref currentMatrix, round);

                for (int column = 1; column < 4; column++)
                {
                    for (int line = 0; line < 4; line++)
                        currentMatrix[line, column] = (byte)(this.KeySchedule[line + ((round - 1) * 4), column] ^ currentMatrix[line, column - 1]);
                }
                ApplyRound(round, 4 * round, currentMatrix);
            }
        }

        private void DoFirstColumnRound(ref byte[,] currentMatrix, int round)
        {
            int lineIndex = round * 4;
            byte[] firstColumn = new byte[4];
            byte[] lastRoundKey = new byte[4];

            // Passo 1
            for (int index = lineIndex - 4, maximum = index + 4, column = 0; index < maximum; index++, column++)
            {
                firstColumn[column] = this.KeySchedule[index, 3];
                lastRoundKey[column] = this.KeySchedule[index, 0]; // utilizado no passo 6
            }
            // Passo 2
            RotateBytes(ref firstColumn);
            // Passo 3
            ReplaceWord(ref firstColumn);
            // Passo 4
            byte[] roundConstant = RoundConstant[round];
            // Passo 5
            for (int column = 0; column < 4; column++)
                firstColumn[column] = (byte)(firstColumn[column] ^ roundConstant[column]);
            // Passo 6
            for (int column = 0; column < 4; column++)
                firstColumn[column] = (byte)(firstColumn[column] ^ lastRoundKey[column]);

            for (int line = 0; line < 4; line++)
                currentMatrix[line, 0] = firstColumn[line];
        }

        private void RotateBytes(ref byte[] currentColumn)
        {
            byte first = currentColumn[0];

            for (int index = 1; index < 4; index++)
                currentColumn[index - 1] = currentColumn[index];

            currentColumn[3] = first;
        }

        private void ReplaceWord(ref byte[] currentColumn)
        {
            for (int index = 0; index < 4; index++)
            {
                string current = currentColumn[index].ToHexByte();

                if (current.Length != 2)
                    throw new Exception("Erro ao substituir palava: tamanho diferente de 2.");

                int newLine = GetIntValue(current[0].ToString());
                int newColumn = GetIntValue(current[1].ToString());

                currentColumn[index] = SubstitutionBox.Get(newLine, newColumn);
            }
        }

        private int GetIntValue(string value)
        {
            value = value.ToUpper();

            switch (value)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(value);
                case "A":
                    return 10;
                case "B":
                    return 11;
                case "C":
                    return 12;
                case "D":
                    return 13;
                case "E":
                    return 14;
                case "F":
                    return 15;
                default:
                    throw new Exception("Valor inesperado.");
            }
        }

        private void ApplyRound(int round, int beggining, byte[,] matrix)
        {
            for (int line = beggining, index = 0; line < beggining + 4; line++, index++)
                for (int column = 0; column < 4; column++)
                    this.KeySchedule[line, column] = matrix[index, column];

            WriteLog($"Round {round}", matrix);
        }
        #endregion ExpandKeys

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

        public void WriteLog(string step, byte[,] matrix)
        {
            Console.WriteLine($"*** {step} ***");

            for (int line = 0; line < 4; line++)
                Console.WriteLine($"{matrix[line, 0].ToHexByte(true)} {matrix[line, 1].ToHexByte(true)} {matrix[line, 2].ToHexByte(true)} {matrix[line, 3].ToHexByte(true)}");

            Console.WriteLine();
        }
    }

    public static class Extentions
    {
        public static string ToHexByte(this byte value, bool withPrefix = false)
        {
            try
            {
                return (withPrefix ? "0x" : string.Empty) + value.ToString("x2");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
