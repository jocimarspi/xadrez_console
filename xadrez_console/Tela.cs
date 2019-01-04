using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            ImprimirTabuleiro(tabuleiro, null);
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] movimentosPossiveis)
        {
            Posicao posicao = new Posicao(0, 0);
            for (int linha = 0; linha < tabuleiro.Linhas; linha++)
            {
                Console.Write(tabuleiro.Linhas - linha + " ");
                for (int coluna = 0; coluna < tabuleiro.Colunas; coluna++)
                {
                    posicao.DefinirPosicao(linha, coluna);
                    imprimirPeca(tabuleiro, posicao, movimentosPossiveis);
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h ");
        }

        public static void imprimirPeca(Tabuleiro tabuleiro, Posicao posicao, bool[,] movimentosPossiveis)
        {
            ConsoleColor corFundoAnterior = Console.BackgroundColor;
            ConsoleColor corFundoMovimentoPossivel = ConsoleColor.DarkGray;

            try
            {
                Peca peca = tabuleiro.peca(posicao);

                if (movimentosPossiveis != null && movimentosPossiveis[posicao.Linha, posicao.Coluna])
                    Console.BackgroundColor = corFundoMovimentoPossivel;

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
            finally
            {
                Console.BackgroundColor = corFundoAnterior;
            }
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

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            foreach (Peca p in conjunto)
            {
                Console.Write(p + " ");
            }

        }

        public static void ImprimirPecasCapturadasBrancas(PartidaDeXadrez partida)
        {
            Console.Write("Brancas: ");
            Console.Write("[");

            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));

            Console.WriteLine("]");
        }

        public static void ImprimirPecasCapturadasPretas(PartidaDeXadrez partida)
        {
            Console.Write("Pretas: ");
            Console.Write("[");
            ConsoleColor corFonteAnterior = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;

            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));

            Console.ForegroundColor = corFonteAnterior;
            Console.WriteLine("]");
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");

            ImprimirPecasCapturadasBrancas(partida);
            ImprimirPecasCapturadasPretas(partida);
        }

        public static void ImprimirSituacaoPartida(PartidaDeXadrez partida)
        {
            if (partida.Terminada)
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.JogadaAtual);
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Aguardando jogada das peças: " + partida.JogadaAtual);

            if (partida.EmXeque)
                Console.WriteLine("XEQUE!");
        }

        public static void ImprimirPartida(PartidaDeXadrez partida, bool[,] movimentosPossiveis)
        {
            ImprimirTabuleiro(partida.Tabuleiro, movimentosPossiveis);

            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);

            ImprimirSituacaoPartida(partida); 
        }

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirPartida(partida, null);
        }
    }
}
