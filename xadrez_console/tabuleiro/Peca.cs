namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QuantidadeMovimentos = 0;
        }

        public int IncrementarQuantidadeMovimentos()
        {
            return QuantidadeMovimentos++;
        }

        public int  DecrementarQuantidadeMovimentos()
        {
            return QuantidadeMovimentos--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] movimentosPossiveis = RetornarMovimetacoesPossiveis();

            for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
            {
                for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                {
                    if (movimentosPossiveis[linha, coluna])
                        return true;
                }
            }

            return false;
        }

        public bool MovimentoPossivel(Posicao pos)
        {
            return RetornarMovimetacoesPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] RetornarMovimetacoesPossiveis();
    }
}
