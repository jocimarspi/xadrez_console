using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(tabuleiro.Linhas - linha + " ");
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    Peca peca = tabuleiro.peca(linha, coluna);
                    imprimirPeca(peca);                    
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h ");
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                imprimirCasaVazia();
                return;
            }

            if (peca.Cor == Cor.Branca)
                imprimirPecaBranca(peca);
            else
                imprimirPecaPreta(peca);
        }

        public static void imprimirPecaBranca(Peca peca)
        {
            if (peca == null)
                return;

            Console.Write(peca + " ");
        }

        public static void imprimirPecaPreta(Peca peca)
        {
            if (peca == null)
                return;

            ConsoleColor corAnterior = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(peca + " ");
            Console.ForegroundColor = corAnterior;
        }

        public static void imprimirCasaVazia()
        {
            Console.Write("- ");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string posicaoInformada = Console.ReadLine();

            char coluna = posicaoInformada[0];
            int linha = (int)char.GetNumericValue(posicaoInformada[1]);           

            return new PosicaoXadrez(coluna,linha);
        }
    }
}
