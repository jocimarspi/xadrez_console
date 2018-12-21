using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    Peca peca = tabuleiro.peca(linha, coluna);

                    string casa = "-";
                    if (peca != null)
                    {
                        casa = peca.ToString();
                    }

                    Console.Write(casa+" ");
                }
                Console.WriteLine();
            }
        }

    }
}
