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
            byte[,] result = matrix;

            for (int line = 0; line < 4; line++)
            {
                for (int column = 0; column < 4; column++)
                {
                    result[line, column] = (byte)(GetGalois(matrix[0, line], MultiplicationMatrix[0][column]) ^
                                                  GetGalois(matrix[1, line], MultiplicationMatrix[1][column]) ^
                                                  GetGalois(matrix[2, line], MultiplicationMatrix[2][column]) ^
                                                  GetGalois(matrix[3, line], MultiplicationMatrix[3][column]));
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
