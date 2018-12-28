using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadaAtual { get; set; }
        public bool Terminada { get; set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8,8);
            Turno = 1;
            JogadaAtual = Cor.Branca;
            Terminada = false;

            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            iniciarTabuleiro();
        }

        public void ValidarPosicaoOrigem(Posicao origem)
        {
            Tabuleiro.ValidarPosicao(origem);

            Peca peca = Tabuleiro.peca(origem);

            if (peca == null)
            {
                throw new TabuleiroException("Não existe peça na possição de origem escolhida.");
            }            

            if (JogadaAtual != peca.Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua.");
            }

            if (!peca.ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existe movimentos possíveis para peça de origem escolhida.");
            }
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca pecaMovida = Tabuleiro.RetirarPecao(origem);
            pecaMovida.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPecao(destino);
            Tabuleiro.ColocarPeca(pecaMovida, destino);

            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);
        }

        public void MudarJogada()
        {
            JogadaAtual = JogadaAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            MudarJogada();
            Turno++;
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida.");
            }
        }

        private void ColocarPeca(Peca peca, char coluna, int linha)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> cap = new HashSet<Peca>();

            foreach (Peca p in capturadas)
            {
                if (p.Cor == cor)
                    cap.Add(p);
            }

            return cap;
            
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecasEmJogo = new HashSet<Peca>();

            foreach (Peca p in pecas)
            {
                if (p.Cor == cor)
                    pecasEmJogo.Add(p);
            }

            pecasEmJogo.ExceptWith(PecasCapturadas(cor));

            return pecasEmJogo;
        }

        private void iniciarPecasBrancas()
        {
            ColocarPeca(new Rei(Tabuleiro, Cor.Branca), 'e', 1);
            ColocarPeca(new Torre(Tabuleiro, Cor.Branca), 'a', 1);


            /*ColocarPeca(new Torre(Tabuleiro, Cor.Branca), 'a', 1);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Branca), 'b', 1);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Branca), 'c', 1);
            ColocarPeca(new Dama(Tabuleiro, Cor.Branca), 'd', 1);
            ColocarPeca(new Rei(Tabuleiro, Cor.Branca), 'e', 1);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Branca), 'f', 1);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Branca), 'g', 1);
            ColocarPeca(new Torre(Tabuleiro, Cor.Branca), 'h', 1);

            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'a', 2);
            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'b', 2);
            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'c', 2);
            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'd', 2);
            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'e', 2);
            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'f', 2);
            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'g', 2);
            ColocarPeca(new Peao(Tabuleiro, Cor.Branca), 'h', 2);*/            
        }
        private void iniciarPecasPretas()
        {
            ColocarPeca(new Torre(Tabuleiro, Cor.Preta), 'a', 8);
            ColocarPeca(new Rei(Tabuleiro, Cor.Preta), 'e', 8);

            /*ColocarPeca(new Torre(Tabuleiro, Cor.Preta), 'a', 8);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Preta), 'b', 8);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Preta), 'c', 8);
            ColocarPeca(new Dama(Tabuleiro, Cor.Preta), 'd', 8);
            ColocarPeca(new Rei(Tabuleiro, Cor.Preta), 'e', 8);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Preta), 'f', 8);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Preta), 'g', 8);
            ColocarPeca(new Torre(Tabuleiro, Cor.Preta), 'h', 8);

            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'a', 7);
            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'b', 7);
            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'c', 7);
            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'd', 7);
            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'e', 7);
            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'f', 7);
            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'g', 7);
            ColocarPeca(new Peao(Tabuleiro, Cor.Preta), 'h', 7);*/
        }

        private void iniciarTabuleiro()
        {
            iniciarPecasBrancas();
            iniciarPecasPretas();
        }        
    }
}
