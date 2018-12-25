using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        private bool PodeMover(Posicao pos)
        {
            Peca pecaNaPosicao = Tabuleiro.peca(pos);
            return pecaNaPosicao == null || pecaNaPosicao.Cor != Cor;
        }

        private void DefinirPosicaoAcima(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha - 1, pos.Coluna);
            }
        }

        private void DefinirPosicaoAbaixo(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha + 1, pos.Coluna);
            }

        }

        private void DefinirPosicaoEsquerda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha, pos.Coluna - 1);
            }
        }

        private void DefinirPosicaoDireita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);

            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null)
                    break;

                pos.DefinirPosicao(pos.Linha, pos.Coluna + 1);
            }
        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            Posicao posicao = new Posicao(0, 0);
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            DefinirPosicaoAcima(posicao, movimentosPossiveis);
            DefinirPosicaoAbaixo(posicao, movimentosPossiveis);
            DefinirPosicaoEsquerda(posicao, movimentosPossiveis);
            DefinirPosicaoDireita(posicao, movimentosPossiveis);

            return movimentosPossiveis;
        }



        public override string ToString()
        {
            return "T";
        }
    }
}
