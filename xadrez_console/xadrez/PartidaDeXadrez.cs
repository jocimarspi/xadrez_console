﻿using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadaAtual { get; set; }
        public bool Terminada { get; set; }
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;
        public Peca PecaVuneravelEnPassant { get; private set; }
        public bool EmXeque { get; set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadaAtual = Cor.Branca;
            Terminada = false;

            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();

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

        private bool RoqueMenor(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            return pecaMovida is Rei && destino.Coluna == origem.Coluna + 2;
        }

        private void ExecutarMovimentoRoqueMenor(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

            Peca torre = Tabuleiro.RetirarPecao(origemTorre);
            torre.IncrementarQuantidadeMovimentos();
            Tabuleiro.ColocarPeca(torre, destinoTorre);
        }

        private bool RoqueMaior(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            return pecaMovida is Rei && destino.Coluna == origem.Coluna - 2;
        }

        private void ExecutarMovimentoRoqueMaior(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

            Peca torre = Tabuleiro.RetirarPecao(origemTorre);
            torre.IncrementarQuantidadeMovimentos();
            Tabuleiro.ColocarPeca(torre, destinoTorre);
        }

        private void ExecutarMovimentoRoque(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            if (RoqueMenor(pecaMovida, origem, destino))
                ExecutarMovimentoRoqueMenor(pecaMovida, origem, destino);

            if (RoqueMaior(pecaMovida, origem, destino))
                ExecutarMovimentoRoqueMaior(pecaMovida, origem, destino);
        }

        private Peca ExecutarCapturaEnPassant(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            if (!(pecaMovida is Peao))
                return null;

            if (origem.Coluna == destino.Coluna)
                return null;

            Posicao posicaoPeao;
            if (pecaMovida.Cor == Cor.Branca)
                posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
            else
                posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);

            Peca peaoCapturado = Tabuleiro.RetirarPecao(posicaoPeao);
            _capturadas.Add(peaoCapturado);

            return peaoCapturado;
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca pecaMovida = Tabuleiro.RetirarPecao(origem);
            pecaMovida.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPecao(destino);
            Tabuleiro.ColocarPeca(pecaMovida, destino);

            if (pecaCapturada != null)
                _capturadas.Add(pecaCapturada);
            else
                pecaCapturada = ExecutarCapturaEnPassant(pecaMovida, origem, destino);

            ExecutarMovimentoRoque(pecaMovida, origem, destino);

            return pecaCapturada;
        }

        public void MudarJogada()
        {
            JogadaAtual = JogadaAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private void DesfazerMovimentoRoqueMenor(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

            Peca torre = Tabuleiro.RetirarPecao(destinoTorre);
            torre.IncrementarQuantidadeMovimentos();
            Tabuleiro.ColocarPeca(torre, origemTorre);
        }

        private void DesfazerMovimentoRoqueMaior(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
            Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

            Peca torre = Tabuleiro.RetirarPecao(destinoTorre);
            torre.IncrementarQuantidadeMovimentos();
            Tabuleiro.ColocarPeca(torre, origemTorre);
        }

        private void DesfazerMovimentoRoque(Peca pecaMovida, Posicao origem, Posicao destino)
        {
            if (RoqueMenor(pecaMovida, origem, destino))
                DesfazerMovimentoRoqueMenor(pecaMovida, origem, destino);

            if (RoqueMaior(pecaMovida, origem, destino))
                DesfazerMovimentoRoqueMaior(pecaMovida, origem, destino);
        }

        private void DesfazerMovimentoEnPassant(Peca pecaMovida, Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            if (!(pecaMovida is Peao))
                return;

            if (pecaCapturada != PecaVuneravelEnPassant)
                return;

            if (origem.Coluna == destino.Coluna)
                return;

            Peca peao = Tabuleiro.RetirarPecao(destino);
            Posicao posicaoPeao;

            if (pecaMovida.Cor == Cor.Branca)
                posicaoPeao = new Posicao(3, destino.Coluna);
            else
                posicaoPeao = new Posicao(4, destino.Coluna);

            Tabuleiro.ColocarPeca(peao, posicaoPeao);
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca pecaNoDestino = Tabuleiro.RetirarPecao(destino);
            pecaNoDestino.DecrementarQuantidadeMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }

            Tabuleiro.ColocarPeca(pecaNoDestino, origem);

            DesfazerMovimentoEnPassant(pecaNoDestino, origem, destino, pecaCapturada);
            DesfazerMovimentoRoque(pecaNoDestino, origem, destino);
        }

        private void DefinirPartidaEmXeque()
        {
            EmXeque = EstaEmXeque(Adversaria(JogadaAtual));
            Terminada = EstaEmXequemate(Adversaria(JogadaAtual));
        }

        private void DefinirPecaVuneravelEnPassant(Posicao origem, Posicao destino)
        {
            PecaVuneravelEnPassant = null;

            Peca pecaMovimentada = Tabuleiro.peca(destino);

            if (!(pecaMovimentada is Peao))
                return;

            if (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)
                PecaVuneravelEnPassant = pecaMovimentada;
        }

        private bool PecaBrancaChegouAoFinal(Peca pecaMovida)
        {
            return pecaMovida.Cor == Cor.Branca && pecaMovida.Posicao.Linha == 0;                
        }

        private bool PecaPretaChegouAoFinal(Peca pecaMovida)
        {
            return (pecaMovida.Cor == Cor.Preta && pecaMovida.Posicao.Linha == 7);
        }

        private bool PeaoChegouAoFinal(Peca pecaMovida)
        {
            if (!(pecaMovida is Peao))
                return false;

            return PecaBrancaChegouAoFinal(pecaMovida) || PecaPretaChegouAoFinal(pecaMovida);
        }

        private void DefinirPromocaoPeao(Peca pecaMovida)
        {
            if (!PeaoChegouAoFinal(pecaMovida))
                return;

            Posicao posicaoPecaMovida = pecaMovida.Posicao;

            pecaMovida = Tabuleiro.RetirarPecao(posicaoPecaMovida);
            _pecas.Remove(pecaMovida);
            Peca dama = new Dama(Tabuleiro, pecaMovida.Cor);
            Tabuleiro.ColocarPeca(dama, posicaoPecaMovida);
            _pecas.Add(dama);
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            DefinirPromocaoPeao(Tabuleiro.peca(destino));

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

            DefinirPecaVuneravelEnPassant(origem, destino);
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.peca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino inválida.");

        }

        private void ColocarPeca(Peca peca, char coluna, int linha)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> cap = new HashSet<Peca>();

            foreach (Peca p in _capturadas)
            {
                if (p.Cor == cor)
                    cap.Add(p);
            }

            return cap;

        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> pecasEmJogo = new HashSet<Peca>();

            foreach (Peca p in _pecas)
            {
                if (p.Cor == cor)
                    pecasEmJogo.Add(p);
            }

            pecasEmJogo.ExceptWith(PecasCapturadas(cor));

            return pecasEmJogo;
        }

        private void IniciarPecasBrancas()
        {

            ColocarPeca(new Torre(Tabuleiro, Cor.Branca), 'a', 1);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Branca), 'b', 1);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Branca), 'c', 1);
            ColocarPeca(new Dama(Tabuleiro, Cor.Branca), 'd', 1);
            ColocarPeca(new Rei(this, Cor.Branca), 'e', 1);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Branca), 'f', 1);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Branca), 'g', 1);
            ColocarPeca(new Torre(Tabuleiro, Cor.Branca), 'h', 1);

            ColocarPeca(new Peao(this, Cor.Branca), 'a', 2);
            ColocarPeca(new Peao(this, Cor.Branca), 'b', 2);
            ColocarPeca(new Peao(this, Cor.Branca), 'c', 2);
            ColocarPeca(new Peao(this, Cor.Branca), 'd', 2);
            ColocarPeca(new Peao(this, Cor.Branca), 'e', 6);
            ColocarPeca(new Peao(this, Cor.Branca), 'f', 2);
            ColocarPeca(new Peao(this, Cor.Branca), 'g', 2);
            ColocarPeca(new Peao(this, Cor.Branca), 'h', 2);
        }
        private void IniciarPecasPretas()
        {
            ColocarPeca(new Torre(Tabuleiro, Cor.Preta), 'a', 8);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Preta), 'b', 8);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Preta), 'c', 8);
            ColocarPeca(new Dama(Tabuleiro, Cor.Preta), 'd', 8);
            ColocarPeca(new Rei(this, Cor.Preta), 'e', 8);
            ColocarPeca(new Bispo(Tabuleiro, Cor.Preta), 'f', 8);
            ColocarPeca(new Cavalo(Tabuleiro, Cor.Preta), 'g', 8);
            ColocarPeca(new Torre(Tabuleiro, Cor.Preta), 'h', 8);

            ColocarPeca(new Peao(this, Cor.Preta), 'a', 7);
            ColocarPeca(new Peao(this, Cor.Preta), 'b', 7);
            ColocarPeca(new Peao(this, Cor.Preta), 'c', 7);
            ColocarPeca(new Peao(this, Cor.Preta), 'd', 3);
            ColocarPeca(new Peao(this, Cor.Preta), 'e', 7);
            ColocarPeca(new Peao(this, Cor.Preta), 'f', 7);
            ColocarPeca(new Peao(this, Cor.Preta), 'g', 7);
            ColocarPeca(new Peao(this, Cor.Preta), 'h', 7);
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

        private bool EstaEmXeque(Cor cor)
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
