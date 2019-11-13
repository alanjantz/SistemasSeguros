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

        public static byte[,] Mix(byte[,] matrix, Key key)
        {
            int qtdLinhas = matrix.Length / key.Size;
            byte[,] result = new byte[qtdLinhas, key.Size];

            for (int block = 0; block < qtdLinhas / key.Size; block++)
            {
                int beggining = block * key.Size;
                for (int column = 0; column < key.Size; column++)
                {
                    for (int line = beggining; line < beggining + key.Size; line++)
                    {
                        result[line, column] = (byte)(GetGalois(matrix[beggining + 0, column], MultiplicationMatrix[line % key.Size][0]) ^
                                                      GetGalois(matrix[beggining + 1, column], MultiplicationMatrix[line % key.Size][1]) ^
                                                      GetGalois(matrix[beggining + 2, column], MultiplicationMatrix[line % key.Size][2]) ^
                                                      GetGalois(matrix[beggining + 3, column], MultiplicationMatrix[line % key.Size][3]));
                    }
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
