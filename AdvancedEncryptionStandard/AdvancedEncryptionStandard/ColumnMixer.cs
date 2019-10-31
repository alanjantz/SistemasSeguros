namespace AdvancedEncryptionStandard
{
    public static class ColumnMixer
    {
        private static readonly TableL TableL = new TableL();
        private static readonly TableE TableE = new TableE();

        private static readonly byte[][] MultiplicationMatrix =
            new byte[][]
            {
                new byte[] { 2, 3, 1, 1 },
                new byte[] { 1, 2, 3, 1 },
                new byte[] { 1, 1, 2, 3 },
                new byte[] { 3, 1, 1, 2 }
            };

        public static byte[,] Mix(byte[,] matrix)
        {
            byte[,] result = new byte[4, 4];

            for (int column = 0; column < 4; column++)
            {
                for (int line = 0; line < 4; line++)
                {
                    result[column, line] = (byte)(GetGalois(matrix[0, column], MultiplicationMatrix[column][0]) ^ // verificar posições
                                                  GetGalois(matrix[1, column], MultiplicationMatrix[column][1]) ^ // verificar posições
                                                  GetGalois(matrix[2, column], MultiplicationMatrix[column][2]) ^ // verificar posições
                                                  GetGalois(matrix[3, column], MultiplicationMatrix[column][3])); // verificar posições
                }
            }

            return result;
        }

        private static byte GetGalois(byte first, byte second)
        {
            first = TableL.GetNewByte(first);
            second = TableL.GetNewByte(second);

            byte result = (byte)((first + second) % 0xFF);

            result = TableE.GetNewByte(result);

            MatrixTable.GetNewPositions(out int firstTerm, out int secondTerm, result.ToHexByte());

            if (firstTerm == 0 || secondTerm == 0)
                return 0;
            else if (firstTerm == 1)
                return (byte)secondTerm;
            else if (secondTerm == 1)
                return (byte)firstTerm;
            else
                return result;
        }
    }
}
