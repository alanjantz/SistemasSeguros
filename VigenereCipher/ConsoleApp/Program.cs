using Vigenere;
using System;

namespace ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var opcao = PegarOpcao();

                var chave = ObterChave();

                Console.WriteLine();
                Console.WriteLine("Informe o texto:");
                var texto = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Novo texto:");

                if (opcao == "C")
                    Console.WriteLine(Vigenere.Vigenere.Cifrar(chave, texto));
                else if (opcao == "D")
                    Console.WriteLine(Vigenere.Vigenere.Decifrar(chave, texto));

                Console.WriteLine();
            }
        }

        private static string ObterChave()
        {
            Console.WriteLine("Informe a chave:");
            var chave = Console.ReadLine();

            if (chave != null)
                chave = chave.ToUpper().Trim();

            return chave;
        }

        private static string PegarOpcao()
        {
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("'C' - Cifrar");
            Console.WriteLine("'D' - Decifrar");
            Console.WriteLine();
            var opcao = Console.ReadLine();

            if (opcao != null)
                opcao = opcao.ToUpper().Trim();

            if (opcao != "C" && opcao != "D")
                throw new Exception("Opção inválida!");

            return opcao;
        }
    }
}
