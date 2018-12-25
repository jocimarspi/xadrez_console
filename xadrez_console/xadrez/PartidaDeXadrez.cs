using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; set; }
        public Cor JogadaAtual { get; set; }
        public bool Terminada { get; set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8,8);
            Turno = 1;
            JogadaAtual = Cor.Branca;
            Terminada = false;

            iniciarTabuleiro();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca pecaMovida = Tabuleiro.RetirarPecao(origem);
            pecaMovida.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPecao(destino);
            Tabuleiro.ColocarPeca(pecaMovida, destino);
        }

        private void iniciarPecasBrancas()
        {
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('f', 8).ToPosicao());

            /*Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('a', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Cavalo(Tabuleiro, Cor.Branca), new PosicaoXadrez('b', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Bispo(Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Dama(Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Bispo(Tabuleiro, Cor.Branca), new PosicaoXadrez('f', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Cavalo(Tabuleiro, Cor.Branca), new PosicaoXadrez('g', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('h', 1).ToPosicao());*/

            /*Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('a', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('b', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('f', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('g', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Branca), new PosicaoXadrez('h', 2).ToPosicao());*/
        }
        private void iniciarPecasPretas()
        {
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            /*Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('a', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Cavalo(Tabuleiro, Cor.Preta), new PosicaoXadrez('b', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Bispo(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Dama(Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Bispo(Tabuleiro, Cor.Preta), new PosicaoXadrez('f', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Cavalo(Tabuleiro, Cor.Preta), new PosicaoXadrez('g', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('h', 8).ToPosicao());

            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('a', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('b', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('f', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('g', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Peao(Tabuleiro, Cor.Preta), new PosicaoXadrez('h', 7).ToPosicao());*/
        }

        private void iniciarTabuleiro()
        {
            iniciarPecasBrancas();
            iniciarPecasPretas();
        }        
    }
}
