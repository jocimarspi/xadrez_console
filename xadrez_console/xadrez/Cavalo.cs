using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            return new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
        }

        public override string ToString()
        {
            return "C";
        }

    }
}
