using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vigenere
{
    public static class Vigenere
    {
        public static readonly List<char> Alfabeto = Enumerable.Range('A', 26).Select(x => (char)x).ToList();

        private enum OperationType
        {
            Somar, Diminuir
        }

        public static string Cifrar(string chave, string valor)
        {
            return Cipher(chave, valor, OperationType.Somar);
        }

        public static string Decifrar(string chave, string valor)
        {
            return Cipher(chave, valor, OperationType.Diminuir);
        }

        private static string Cipher(string chave, string valor, OperationType type)
        {
            chave = chave.RemoverEspacosEmBranco().ToUpper();
            ValidarChave(chave, valor);

            valor = valor.RemoverEspacosEmBranco().ToUpper();
            var filaCaracteresChave = MontarFilaValoresASubstituir(chave, type);

            StringBuilder novoValor = new StringBuilder();

            for (int i = 0; i < valor.Length; i++)
            {
                var corrente = valor[i];

                if (Alfabeto.Contains(corrente))
                {
                    var caractece = filaCaracteresChave.Dequeue();
                    var posicao = Alfabeto.IndexOf(corrente) + caractece;
                    if (posicao >  Alfabeto.Count - 1)
                        posicao -= Alfabeto.Count;
                    else if (posicao < 0)
                        posicao += Alfabeto.Count;

                    novoValor.Append(Alfabeto[posicao]);
                    filaCaracteresChave.Enqueue(caractece);
                }
            }

            return novoValor.ToString().ToUpper();
        }

        private static Queue<int> MontarFilaValoresASubstituir(string chave, OperationType type)
        {
            var fila = new Queue<int>();

            for (int i = 0; i < chave.Length; i++)
            {
                int posicao = 'A';

                int codigo = (chave[i] + posicao) % Alfabeto.Count;

                if (type == OperationType.Diminuir)
                    codigo *= -1;
                fila.Enqueue(codigo);
            }

            return fila;
        }

        private static void ValidarChave(string chave, string valor)
        {
            if (string.IsNullOrWhiteSpace(chave))
                throw new Exception("A chave deve ser informada.");

            if (chave.Length > valor.Length)
                throw new Exception("O valor da chave é maior do que o texto a ser cifrado/decifrado.");

            IList<char> valoresInvalidos = new List<char>();

            for (int i = 0; i < chave.Length; i++)
            {
                if (!Alfabeto.Contains(chave[i]))
                    valoresInvalidos.Add(chave[i]);
            }

            if (valoresInvalidos.Any())
                throw new Exception("A chave contem os seguintes caracteres inválidos: " + string.Join(", ", valoresInvalidos));
        }
    }

    public static class Extentions
    {
        public static string RemoverEspacosEmBranco(this string valor)
        {
            while (valor.Contains(" "))
                valor = valor.Replace(" ", "");

            return valor;
        }

        public static int ToInt(this object valor)
        {
            try
            {
                return Convert.ToInt32(valor);
            }
            catch
            {
                return 0;
            }
        }
    }
}
