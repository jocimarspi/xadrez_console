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
        public bool EmXeque { get; set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadaAtual = Cor.Branca;
            Terminada = false;

            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            IniciarTabuleiro();
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

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca pecaMovida = Tabuleiro.RetirarPecao(origem);
            pecaMovida.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPecao(destino);
            Tabuleiro.ColocarPeca(pecaMovida, destino);

            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);

            return pecaCapturada;
        }

        public void MudarJogada()
        {
            JogadaAtual = JogadaAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca pecaNoDestino = Tabuleiro.RetirarPecao(destino);
            pecaNoDestino.DecrementarQuantidadeMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocarPeca(pecaNoDestino, origem);
        }

        private void DefinirPartidaEmXeque()
        {
            EmXeque = EstaEmXeque(Adversaria(JogadaAtual));
            Terminada = EstaEmXequemate(Adversaria(JogadaAtual));
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(JogadaAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque.");
            }

            DefinirPartidaEmXeque();

            if (!Terminada)
            {
                MudarJogada();
                Turno++;
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.peca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino inválida.");

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

        private void IniciarPecasBrancas()
        {
            ColocarPeca(new Rei(Tabuleiro, Cor.Branca), 'e', 1);
            ColocarPeca(new Torre(Tabuleiro, Cor.Branca), 'a', 1);
            ColocarPeca(new Torre(Tabuleiro, Cor.Branca), 'h', 7);

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
        private void IniciarPecasPretas()
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

        private void IniciarTabuleiro()
        {
            IniciarPecasBrancas();
            IniciarPecasPretas();
        }

        private Cor Adversaria(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca RetornarRei(Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if (p is Rei)
                    return p;
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca Rei = RetornarRei(cor);

            if (Rei == null)
                throw new TabuleiroException("Não tem um rei da cor " + cor + " no tabuleiro!");

            foreach (Peca p in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] movimentosPossiveis = p.RetornarMovimetacoesPossiveis();

                if (movimentosPossiveis[Rei.Posicao.Linha, Rei.Posicao.Coluna])
                    return true;
            }

            return false;
        }

        public bool EstaEmXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
                return false;

            foreach (Peca p in PecasEmJogo(cor))
            {
                bool[,] movimentosPossiveis = p.RetornarMovimetacoesPossiveis();

                for (int linha = 0; linha < Tabuleiro.Linhas; linha++)
                {
                    for (int coluna = 0; coluna < Tabuleiro.Colunas; coluna++)
                    {
                        if (movimentosPossiveis[linha, coluna])
                        {
                            Posicao origem = p.Posicao;
                            Posicao destino = new Posicao(linha, coluna);
                            
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            
                            bool estaEmXeque = EstaEmXeque(cor);

                            DesfazerMovimento(origem, destino, pecaCapturada);

                            if (!estaEmXeque)
                                return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
