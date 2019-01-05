using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        private bool PodeMover(Posicao pos)
        {
            Peca pecaNaPosicao = Tabuleiro.peca(pos);
            return pecaNaPosicao == null || pecaNaPosicao.Cor != Cor;
        }

        private void DefinirPosicao2Acima1Esqueda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicao1Acima2Esqueda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 2);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

        }

        private void DefinirPosicao2Abaixo1Esqueda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

        }

        private void DefinirPosicao1Abaixo2Esqueda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 2);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicao2Acima1Direita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicao1Acima2Direita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 2);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicao2Abaixo1Direita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicao1Abaixo2Direita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 2);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            Posicao posicao = new Posicao(0, 0);
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            DefinirPosicao1Acima2Esqueda(posicao, movimentosPossiveis);
            DefinirPosicao1Acima2Direita(posicao, movimentosPossiveis);
            DefinirPosicao1Abaixo2Esqueda(posicao, movimentosPossiveis);
            DefinirPosicao1Abaixo2Direita(posicao, movimentosPossiveis);
            DefinirPosicao2Acima1Esqueda(posicao, movimentosPossiveis);
            DefinirPosicao2Acima1Direita(posicao, movimentosPossiveis);
            DefinirPosicao2Abaixo1Esqueda(posicao, movimentosPossiveis);
            DefinirPosicao2Abaixo1Direita(posicao, movimentosPossiveis);

            return movimentosPossiveis;
        }

        public override string ToString()
        {
            return "C";
        }

    }
}
