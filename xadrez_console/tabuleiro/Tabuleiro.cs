namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;

            pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public Peca peca(Posicao posicao)
        {
            return peca(posicao.Linha, posicao.Coluna);
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }

            pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RetirarPecao(Posicao posicao)
        {
            Peca pecaNaPosicao = peca(posicao);

            if (pecaNaPosicao != null)
                pecaNaPosicao.Posicao = null;

            pecas[posicao.Linha, posicao.Coluna] = null;
            return pecaNaPosicao;
        }

        public bool PosicaoValida(Posicao posicao)
        {
            return (posicao.Linha >= 0) && (posicao.Coluna >= 0) && (posicao.Linha < Linhas) && (posicao.Coluna < Colunas);
        }

        public void ValidarPosicao(Posicao posicao)
        {
            if (!PosicaoValida(posicao))
            {
                throw new TabuleiroException("Posição inválida");
            }
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return peca(posicao) != null;
        }
    }
}
