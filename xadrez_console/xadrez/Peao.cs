using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public PartidaDeXadrez Partida { get; private set; }

        public Peao(PartidaDeXadrez partida, Cor cor) : base(partida.Tabuleiro, cor)
        {
            Partida = partida;
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

        private void DefinirEnPassantBrancaEsquerda(bool[,] movimentosPossiveis)
        {
            if (Posicao.Linha != 3)
                return;

            Posicao posicaoInimigo = new Posicao(Posicao.Linha, Posicao.Coluna - 1);

            if (!Tabuleiro.PosicaoValida(posicaoInimigo))
                return;

            if (ExisteInimigo(posicaoInimigo) && Tabuleiro.peca(posicaoInimigo) == Partida.PecaVuneravelEnPassant)
                movimentosPossiveis[posicaoInimigo.Linha - 1 , posicaoInimigo.Coluna] = true;
        }

        private void DefinirEnPassantBrancaDireita(bool[,] movimentosPossiveis)
        {
            if (Posicao.Linha != 3)
                return;

            Posicao posicaoInimigo = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

            if (!Tabuleiro.PosicaoValida(posicaoInimigo))
                return;

            if (ExisteInimigo(posicaoInimigo) && Tabuleiro.peca(posicaoInimigo) == Partida.PecaVuneravelEnPassant)
                movimentosPossiveis[posicaoInimigo.Linha - 1, posicaoInimigo.Coluna] = true;
        }

        private void DefinirEnPassantPretaEsquerda(bool[,] movimentosPossiveis)
        {
            if (Posicao.Linha != 4)
                return;

            Posicao posicaoInimigo = new Posicao(Posicao.Linha, Posicao.Coluna - 1);

            if (!Tabuleiro.PosicaoValida(posicaoInimigo))
                return;

            if (ExisteInimigo(posicaoInimigo) && Tabuleiro.peca(posicaoInimigo) == Partida.PecaVuneravelEnPassant)
                movimentosPossiveis[posicaoInimigo.Linha + 1, posicaoInimigo.Coluna] = true;
        }

        private void DefinirEnPassantPretaDireita(bool[,] movimentosPossiveis)
        {
            if (Posicao.Linha != 4)
                return;

            Posicao posicaoInimigo = new Posicao(Posicao.Linha, Posicao.Coluna + 1);

            if (!Tabuleiro.PosicaoValida(posicaoInimigo))
                return;

            if (ExisteInimigo(posicaoInimigo) && Tabuleiro.peca(posicaoInimigo) == Partida.PecaVuneravelEnPassant)
                movimentosPossiveis[posicaoInimigo.Linha + 1, posicaoInimigo.Coluna] = true;
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
                DefinirEnPassantBrancaEsquerda(movimentosPossiveis);
                DefinirEnPassantBrancaDireita(movimentosPossiveis);
            }
            else
            {
                DefinirAvancar1Preta(pos, movimentosPossiveis);
                DefinirAvancar2Preta(pos, movimentosPossiveis);
                DefinirCapturaEsquerdaPreta(pos, movimentosPossiveis);
                DefinirCapturaDireitaPreta(pos, movimentosPossiveis);
                DefinirEnPassantPretaEsquerda(movimentosPossiveis);
                DefinirEnPassantPretaDireita(movimentosPossiveis);
            }

            return movimentosPossiveis;
        }



        public override string ToString()
        {
            return "p";
        }
    }
}
