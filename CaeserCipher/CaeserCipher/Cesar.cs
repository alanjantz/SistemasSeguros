using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaeserCipher
{
    public static class Cesar
    {
        private static readonly List<char> Alfabeto = Enumerable.Range('A', 26).Select(x => (char)x).ToList();

        public static string Cifrar(string mensagem)
        {
            return Cipher(mensagem, 3);
        }

        public static string Decifrar(string mensagem)
        {
            return Cipher(mensagem, -3);
        }

        private static string Cipher(string mensagem, int valorExtra)
        {
            StringBuilder retorno = new StringBuilder();

            mensagem = mensagem.ToUpper();

            for (int i = 0; i < mensagem.Length; i++)
            {
                char caractereAtual = mensagem[i];
                if (Alfabeto.Contains(caractereAtual))
                {
                    int codigo = Alfabeto.IndexOf(caractereAtual) + valorExtra;

                    if (codigo > Alfabeto.Count - 1)
                        codigo -= Alfabeto.Count;
                    else if (codigo < 0)
                        codigo += Alfabeto.Count;

                    caractereAtual = Alfabeto[codigo];
                }

                retorno.Append(caractereAtual);
            }

            return retorno.ToString();
        }
    }
}
