using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
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

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicaoAbaixo(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

        }

        private void DefinirPosicaoEsquerda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;

        }

        private void DefinirPosicaoDireita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicaoSuperiorEsquerda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicaoSuperiorDireita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirPosicaoInferiorEsquerda(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;   
        }

        private void DefinirPosicaoInferiorDireita(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);

            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            Posicao posicao = new Posicao(0, 0);
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas,Tabuleiro.Colunas];

            DefinirPosicaoAcima(posicao,movimentosPossiveis);
            DefinirPosicaoAbaixo(posicao, movimentosPossiveis);
            DefinirPosicaoEsquerda(posicao, movimentosPossiveis);
            DefinirPosicaoDireita(posicao, movimentosPossiveis);
            DefinirPosicaoInferiorEsquerda(posicao, movimentosPossiveis); 
            DefinirPosicaoInferiorDireita(posicao, movimentosPossiveis);
            DefinirPosicaoSuperiorEsquerda(posicao, movimentosPossiveis);
            DefinirPosicaoSuperiorDireita(posicao, movimentosPossiveis);

            return movimentosPossiveis;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
