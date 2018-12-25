using tabuleiro;

namespace xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override bool[,] RetornarMovimetacoesPossiveis()
        {
            return new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
