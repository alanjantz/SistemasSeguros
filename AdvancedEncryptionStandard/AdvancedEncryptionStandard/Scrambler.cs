using AdvancedEncryptionStandard.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedEncryptionStandard
{
    public class Scrambler
    {
        private byte[] OriginalValue
        {
            get
            {
                return StateMatrixIn.ToArray();
            }
            set
            {
                value = AddPadding(value);
                int qtdLinhas = value.Length / 4;
                StateMatrixIn = new byte[qtdLinhas, 4];
                StateMatrixOut = new byte[qtdLinhas, 4];
                InitializeMatrix(value, StateMatrixIn);
                WriteLog("Texto simples", StateMatrixIn);
            }
        }

        private byte[] EncryptedValue
        {
            get
            {
                return StateMatrixOut.ToArray();
            }
        }
        private Key Key { get; set; }
        private byte[,] StateMatrix { get; set; }
        private byte[,] KeySchedule { get; set; }
        private int Rounds { get; set; }
        private byte[,] StateMatrixIn { get; set; }
        private byte[,] StateMatrixOut { get; set; }
        private int CurrentRound { get; set; }

        private SubstitutionBox SubstitutionBox { get; set; }

        public Scrambler()
        {
            Rounds = 10;
            SubstitutionBox = new SubstitutionBox();
        }

        public Scrambler WithKey(string key, int bytes)
        {
            this.Key = new Key(key, bytes);

            return this;
        }

        public byte[] Encrypt(string value)
        {
            return this.Encrypt(Encoding.ASCII.GetBytes(value));
        }

        public byte[] Encrypt(byte[] value)
        {
            OriginalValue = value;

            ExpandKey();
            Round();

            return EncryptedValue;
        }

        #region ExpandKeys
        private void ExpandKey()
        {
            StateMatrix = new byte[4, 4];
            InitializeMatrix(Key.Value, StateMatrix);
            WriteLog("Chave", StateMatrix);
            DoRoundKeys();
        }

        private void DoRoundKeys()
        {
            int qtdColunas = 4;
            int qtdLinhas = 4 * (Rounds + 1); // quantidade de rodadas + chave original

            this.KeySchedule = new byte[qtdLinhas, qtdColunas];

            ApplyRound(0, 0, StateMatrix, false);

            for (int round = 1; round <= Rounds; round++)
            {
                var currentMatrix = new byte[4, 4];
                DoFirstColumnRound(ref currentMatrix, round);

                for (int column = 1; column < 4; column++)
                {
                    for (int line = 0; line < 4; line++)
                        currentMatrix[line, column] = (byte)(this.KeySchedule[line + ((round - 1) * 4), column] ^ currentMatrix[line, column - 1]);
                }
                ApplyRound(round, 4 * round, currentMatrix, false);
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
            byte[] roundConstant = RoundConstant.Get(round);
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

            currentColumn[currentColumn.Length - 1] = first;
        }

        private void ReplaceWord(ref byte[] currentColumn)
        {
            for (int index = 0; index < 4; index++)
                currentColumn[index] = SubstitutionBox.GetNewByte(currentColumn[index]);
        }

        private void ApplyRound(int round, int beggining, byte[,] matrix, bool writeLog = true)
        {
            for (int line = beggining, index = 0; line < beggining + 4; line++, index++)
                for (int column = 0; column < 4; column++)
                    this.KeySchedule[line, column] = matrix[index, column];

            if (writeLog)
                WriteLog($"Round {round}", matrix);
        }
        #endregion ExpandKeys

        private void Round()
        {
            InitializeRound();

            for (CurrentRound = 1; CurrentRound < Rounds; CurrentRound++)
                DoRound();

            FinalizeRound();
        }

        private void InitializeRound()
        {
            CurrentRound = 0;
            byte[,] roundKey = GetRoundKeyTable(CurrentRound);
            AddRoundKey(StateMatrixIn, roundKey);
        }

        private void DoRound()
        {
            SubBytes();
            ShiftRows();
            MixClomuns();
            byte[,] roundKey = GetRoundKeyTable(CurrentRound);
            AddRoundKey(StateMatrixOut, roundKey);
        }

        private void FinalizeRound()
        {
            SubBytes();
            ShiftRows();
            byte[,] roundKey = GetRoundKeyTable(CurrentRound);
            AddRoundKey(StateMatrixOut, roundKey);
        }

        private void AddRoundKey(byte[,] matrix, byte[,] roundKey)
        {
            for (int column = 0; column < 4; column++)
                for (int line = 0; line < matrix.Length / 4; line++)
                    StateMatrixOut[line, column] = (byte)(matrix[line, column] ^ roundKey[line % 4, column]);
            WriteLog($"AddRoundKey - Round {CurrentRound}", StateMatrixOut);
        }

        private void SubBytes()
        {
            for (int column = 0; column < 4; column++)
            {
                for (int line = 0; line < StateMatrixOut.Length / 4; line++)
                    StateMatrixOut[line, column] = SubstitutionBox.GetNewByte(StateMatrixOut[line, column]);
            }
            WriteLog($"SubBytes - Round {CurrentRound}", StateMatrixOut);
        }

        private void ShiftRows()
        {
            byte[] current = null;
            for (int line = 1; line < StateMatrixOut.Length / 4; line++)
            {
                current = new byte[4];

                for (int column = 0; column < 4; column++)
                    current[column] = StateMatrixOut[line, column];

                for (int turns = 0; turns < line % 4; turns++)
                    RotateBytes(ref current);

                for (int column = 0; column < 4; column++)
                    StateMatrixOut[line, column] = current[column];
            }
            WriteLog($"ShiftRows - Round {CurrentRound}", StateMatrixOut);
        }

        private void MixClomuns()
        {
            StateMatrixOut = ColumnMixer.Mix(StateMatrixOut);
            WriteLog($"MixedColumns - Round {CurrentRound}", StateMatrixOut);
        }

        private byte[] AddPadding(byte[] value)
        {
            List<byte> newValue = value.ToList();

            int blockSize = this.Key.BlockSize;
            int extraValues = newValue.Count % blockSize;

            if (extraValues > 0)
            {
                int diff = blockSize - extraValues;
                newValue.AddRange(Enumerable.Repeat((byte)diff, diff));
            }
            else
                newValue.AddRange(Enumerable.Repeat((byte)Key.BlockSize, Key.BlockSize));

            return newValue.ToArray();
        }

        private void InitializeMatrix(string key, byte[,] matrix)
        {
            InitializeMatrix(Encoding.ASCII.GetBytes(key), matrix);
        }

        private void InitializeMatrix(byte[] key, byte[,] matrix)
        {
            for (int block = 0, index = 0; block < matrix.Length / Key.BlockSize; block++)
            {
                int beggining = block * 4;
                for (int column = 0; column < 4; column++)
                    for (int line = beggining; line < beggining + 4; line++, index++)
                        matrix[line, column] = key[index];
            }
        }

        private byte[,] GetRoundKeyTable(int round)
        {
            var result = new byte[4, 4];

            int lineStart = 4 * round;

            for (int column = 0; column < 4; column++)
                for (int line = lineStart; line < lineStart + 4; line++)
                    result[line % 4, column] = KeySchedule[line, column];

            return result;
        }

        public void WriteLog(string step, byte[,] matrix)
        {
            Logger.Write(step, matrix);
        }
    }
}
