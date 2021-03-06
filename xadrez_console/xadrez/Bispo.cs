﻿using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        private bool PodeMover(Posicao pos)
        {
            Peca pecaNaPosicao = Tabuleiro.peca(pos);
            return pecaNaPosicao == null || pecaNaPosicao.Cor != Cor;
        }

        private void DefinirPosicaoAcimaEsqueda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha - 1, pos.Coluna - 1);
            }
        }

        private void DefinirPosicaoAbaixoEsquerda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha + 1, pos.Coluna - 1);
            }

        }

        private void DefinirPosicaoAcimaDireita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha - 1, pos.Coluna + 1);
            }
        }

        private void DefinirPosicaoAbaixoDireita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha + 1, pos.Coluna + 1);
            }
        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            Posicao posicao = new Posicao(0, 0);
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            DefinirPosicaoAcimaEsqueda(posicao, movimentosPossiveis);
            DefinirPosicaoAcimaDireita(posicao, movimentosPossiveis);
            DefinirPosicaoAbaixoEsquerda(posicao, movimentosPossiveis);
            DefinirPosicaoAbaixoDireita(posicao, movimentosPossiveis);

            return movimentosPossiveis;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
