using System.Collections.Generic;
using System.Linq;
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
                int qtdLinhas = value.Length / Key.Size;
                StateMatrixIn = new byte[qtdLinhas, Key.Size];
                StateMatrixOut = new byte[qtdLinhas, Key.Size];
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
            StateMatrix = new byte[Key.Size, Key.Size];
            InitializeMatrix(Key.Value, StateMatrix);
            WriteLog("Chave", StateMatrix);
            DoRoundKeys();
        }

        private void DoRoundKeys()
        {
            int qtdColunas = Key.Size;
            int qtdLinhas = Key.Size * (Rounds + 1); // quantidade de rodadas + chave original

            this.KeySchedule = new byte[qtdLinhas, qtdColunas];

            ApplyRound(0, 0, StateMatrix);

            for (int round = 1; round <= Rounds; round++)
            {
                var currentMatrix = new byte[Key.Size, Key.Size];
                DoFirstColumnRound(ref currentMatrix, round);

                for (int column = 1; column < Key.Size; column++)
                {
                    for (int line = 0; line < Key.Size; line++)
                        currentMatrix[line, column] = (byte)(this.KeySchedule[line + ((round - 1) * Key.Size), column] ^ currentMatrix[line, column - 1]);
                }
                ApplyRound(round, Key.Size * round, currentMatrix);
            }
        }

        private void DoFirstColumnRound(ref byte[,] currentMatrix, int round)
        {
            int lineIndex = round * Key.Size;
            byte[] firstColumn = new byte[Key.Size];
            byte[] lastRoundKey = new byte[Key.Size];

            // Passo 1
            for (int index = lineIndex - Key.Size, maximum = index + Key.Size, column = 0; index < maximum; index++, column++)
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
            for (int column = 0; column < Key.Size; column++)
                firstColumn[column] = (byte)(firstColumn[column] ^ roundConstant[column]);
            // Passo 6
            for (int column = 0; column < Key.Size; column++)
                firstColumn[column] = (byte)(firstColumn[column] ^ lastRoundKey[column]);

            for (int line = 0; line < Key.Size; line++)
                currentMatrix[line, 0] = firstColumn[line];
        }

        private void RotateBytes(ref byte[] currentColumn)
        {
            byte first = currentColumn[0];

            for (int index = 1; index < Key.Size; index++)
                currentColumn[index - 1] = currentColumn[index];

            currentColumn[currentColumn.Length - 1] = first;
        }

        private void ReplaceWord(ref byte[] currentColumn)
        {
            for (int index = 0; index < Key.Size; index++)
                currentColumn[index] = SubstitutionBox.GetNewByte(currentColumn[index]);
        }

        private void ApplyRound(int round, int beggining, byte[,] matrix, bool writeLog = true)
        {
            for (int line = beggining; line < beggining + Key.Size; line++)
                for (int column = 0; column < Key.Size; column++)
                    this.KeySchedule[line, column] = matrix[line % Key.Size, column];

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
            for (int column = 0; column < Key.Size; column++)
                for (int line = 0; line < matrix.Length / Key.Size; line++)
                    StateMatrixOut[line, column] = (byte)(matrix[line, column] ^ roundKey[line % Key.Size, column]);
            WriteLog($"AddRoundKey - Round {CurrentRound}", StateMatrixOut);
        }

        private void SubBytes()
        {
            for (int column = 0; column < Key.Size; column++)
            {
                for (int line = 0; line < StateMatrixOut.Length / Key.Size; line++)
                    StateMatrixOut[line, column] = SubstitutionBox.GetNewByte(StateMatrixOut[line, column]);
            }
            WriteLog($"SubBytes - Round {CurrentRound}", StateMatrixOut);
        }

        private void ShiftRows()
        {
            byte[] current = null;
            for (int line = 1; line < StateMatrixOut.Length / Key.Size; line++)
            {
                current = new byte[Key.Size];

                for (int column = 0; column < Key.Size; column++)
                    current[column] = StateMatrixOut[line, column];

                for (int turns = 0; turns < line % Key.Size; turns++)
                    RotateBytes(ref current);

                for (int column = 0; column < Key.Size; column++)
                    StateMatrixOut[line, column] = current[column];
            }
            WriteLog($"ShiftRows - Round {CurrentRound}", StateMatrixOut);
        }

        private void MixClomuns()
        {
            StateMatrixOut = ColumnMixer.Mix(StateMatrixOut, Key);
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
                int beggining = block * Key.Size;
                for (int column = 0; column < Key.Size; column++)
                    for (int line = beggining; line < beggining + Key.Size; line++, index++)
                        matrix[line, column] = key[index];
            }
        }

        private byte[,] GetRoundKeyTable(int round)
        {
            var result = new byte[Key.Size, Key.Size];

            int lineStart = Key.Size * round;

            for (int column = 0; column < Key.Size; column++)
                for (int line = lineStart; line < lineStart + Key.Size; line++)
                    result[line % Key.Size, column] = KeySchedule[line, column];

            return result;
        }

        public void WriteLog(string step, byte[,] matrix)
        {
            Logger.Write(step, matrix);
        }
    }
}
