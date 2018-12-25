using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            return new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
