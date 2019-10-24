using System;

namespace AdvancedEncryptionStandard
{
    public abstract class MatrixTable
    {
        public abstract byte[,] Table { get; }

        public byte Get(int line, int column)
        {
            return Table[line, column];
        }

        public virtual byte GetNewByte(byte value)
        {
            string current = value.ToHexByte();

            if (current.Length != 2)
                throw new Exception("Erro ao substituir palava: tamanho diferente de 2.");

            GetNewPositions(out int newLine, out int newColumn, current);
            
            return Get(newLine, newColumn);
        }

        public static void GetNewPositions(out int newLine, out int newColumn, string value)
        {
            newLine = value[0].ToString().ToInt();
            newColumn = value[1].ToString().ToInt();
        }
    }
}
