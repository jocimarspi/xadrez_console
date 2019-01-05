using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez _partida;

        public Rei(PartidaDeXadrez partida, Cor cor) : base(partida.Tabuleiro, cor)
        {
            _partida = partida; 
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

        private bool TorrePodeFazerRoque(Posicao posicaoTorre)
        {
            Peca p = Tabuleiro.peca(posicaoTorre);
            return p != null && p is Torre && p.Cor == Cor && p.QuantidadeMovimentos == 0;
        }

        private bool ExisteOutrasPecasCasasRoqueMenor()
        {
            Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
            Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

            return Tabuleiro.ExistePeca(p1) || Tabuleiro.ExistePeca(p2);
        }

        private bool ExisteOutrasPecasCasasRoqueMaior()
        {
            Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
            Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
            Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

            return Tabuleiro.ExistePeca(p1) || Tabuleiro.ExistePeca(p2) || Tabuleiro.ExistePeca(p3);
        }

        private void DefinirMovimentoRoqueMenor(Posicao pos, bool[,] movimentosPossiveis)
        {
            if (QuantidadeMovimentos > 0 || _partida.EmXeque)
                return;

            Posicao posicaoTorre = new Posicao(Posicao.Linha, Posicao.Coluna + 3);

            if (!TorrePodeFazerRoque(posicaoTorre))
                return;

            if (!ExisteOutrasPecasCasasRoqueMenor())
                movimentosPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
        }

        private void DefinirMovimentoRoqueMaior(Posicao pos, bool[,] movimentosPossiveis)
        {
            if (QuantidadeMovimentos > 0 || _partida.EmXeque)
                return;

            Posicao posicaoTorre = new Posicao(Posicao.Linha, Posicao.Coluna - 4);

            if (!TorrePodeFazerRoque(posicaoTorre))
                return;

            if (!ExisteOutrasPecasCasasRoqueMaior())
                movimentosPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            Posicao posicao = new Posicao(0, 0);
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            DefinirPosicaoAcima(posicao, movimentosPossiveis);
            DefinirPosicaoAbaixo(posicao, movimentosPossiveis);
            DefinirPosicaoEsquerda(posicao, movimentosPossiveis);
            DefinirPosicaoDireita(posicao, movimentosPossiveis);
            DefinirPosicaoInferiorEsquerda(posicao, movimentosPossiveis);
            DefinirPosicaoInferiorDireita(posicao, movimentosPossiveis);
            DefinirPosicaoSuperiorEsquerda(posicao, movimentosPossiveis);
            DefinirPosicaoSuperiorDireita(posicao, movimentosPossiveis);
            DefinirMovimentoRoqueMenor(posicao, movimentosPossiveis);
            DefinirMovimentoRoqueMaior(posicao, movimentosPossiveis);

            return movimentosPossiveis;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
