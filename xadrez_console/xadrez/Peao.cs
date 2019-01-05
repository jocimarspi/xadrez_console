using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {

        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return !EstaLivre(pos) && p.Cor != Cor;
        }

        private bool EstaLivre(Posicao pos)
        {
            return Tabuleiro.peca(pos) == null;
        }

        private void DefinirAvancar1Branca(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && EstaLivre(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirAvancar2Branca(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 2, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && EstaLivre(pos) && QuantidadeMovimentos == 0)
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirCapturaEsquerdaBranca(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirCapturaDireitaBranca(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirAvancar1Preta(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && EstaLivre(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirAvancar2Preta(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 2, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && EstaLivre(pos) && QuantidadeMovimentos == 0)
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirCapturaEsquerdaPreta(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        private void DefinirCapturaDireitaPreta(Posicao pos, bool[,] movimentosPossiveis)
        {
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && ExisteInimigo(pos))
                movimentosPossiveis[pos.Linha, pos.Coluna] = true;
        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                DefinirAvancar1Branca(pos, movimentosPossiveis);
                DefinirAvancar2Branca(pos, movimentosPossiveis);
                DefinirCapturaEsquerdaBranca(pos, movimentosPossiveis);
                DefinirCapturaDireitaBranca(pos, movimentosPossiveis);
            }
            else
            {
                DefinirAvancar1Preta(pos, movimentosPossiveis);
                DefinirAvancar2Preta(pos, movimentosPossiveis);
                DefinirCapturaEsquerdaPreta(pos, movimentosPossiveis);
                DefinirCapturaDireitaPreta(pos, movimentosPossiveis);
            }

            return movimentosPossiveis;
        }



        public override string ToString()
        {
            return "p";
        }
    }
}
