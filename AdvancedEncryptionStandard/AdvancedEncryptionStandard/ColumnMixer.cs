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
            int qtdLinhas = matrix.Length / 4;
            byte[,] result = new byte[qtdLinhas, 4];

            for (int column = 0; column < 4; column++)
            {
                for (int line = 0; line < qtdLinhas; line++)
                {
                    result[line, column] = (byte)(GetGalois(matrix[0, column], MultiplicationMatrix[line % 4][0]) ^
                                                  GetGalois(matrix[1, column], MultiplicationMatrix[line % 4][1]) ^
                                                  GetGalois(matrix[2, column], MultiplicationMatrix[line % 4][2]) ^
                                                  GetGalois(matrix[3, column], MultiplicationMatrix[line % 4][3]));
                }
            }

            return result;
        }

        private static byte GetGalois(byte first, byte second)
        {
            if (first == 0 || second == 0)
                return 0;
            else if (first == 1)
                return (byte)second;
            else if (second == 1)
                return (byte)first;

            first = TableL.GetNewByte(first);
            second = TableL.GetNewByte(second);

            byte result = (byte)((first + second) % 0xFF);

            result = TableE.GetNewByte(result);
            
            return result;
        }
    }
}
