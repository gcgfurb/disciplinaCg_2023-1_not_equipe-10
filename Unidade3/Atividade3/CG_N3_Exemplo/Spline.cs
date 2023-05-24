#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace gcgcg
{
  internal class Spline : Objeto
  {
    private int ptoSelecionado = 0;
    private Ponto[] ptosControle = new Ponto[4];
    private Poligono poliedroControle;

    private readonly int splineQtdPtoMax = 10;  // TODO: definir o tamanho de bezierMatrizPeso
    private int splineQtdPto = 10;
    public void SplineQtdPto(int inc)
    {
      splineQtdPto = (inc < 0 && splineQtdPto > 0) ? splineQtdPto += inc : splineQtdPto;
      splineQtdPto = (inc > 0 && splineQtdPto < splineQtdPtoMax) ? splineQtdPto += inc : splineQtdPto;
      BezierMatrizPeso();
      Atualizar();
    }

    private double[,] bezierMatrizPeso = new double[11, 4];  // FIXME: por que 11 e não 10
    private void BezierMatrizPeso()                          // FIXME: desenha um segmento a mais no final
    {
      int i = 0;
      for (double t = 0; t <= 1; t += 1.0 / splineQtdPto)   // 1.0 para não dar divisão por inteiro
      {
        bezierMatrizPeso[i, 0] = Math.Pow((1 - t), 3);
        bezierMatrizPeso[i, 1] = 3 * t * Math.Pow(1 - t, 2);
        bezierMatrizPeso[i, 2] = 3 * Math.Pow(t, 2) * (1 - t);
        bezierMatrizPeso[i, 3] = Math.Pow(t, 3);
        i++;
      }
    }

    public Spline(Objeto paiRef, ref char _rotulo) : base(paiRef, ref _rotulo)
    {
      PontosControle();
      PoliedroControle();

      PrimitivaTipo = PrimitiveType.LineStrip;
      BezierMatrizPeso();
      for (int i = 0; i <= splineQtdPto; i++)
      {
        base.PontosAdicionar(new Ponto4D());
      }
      Atualizar();
    }

    private void PontosControle()
    {
      char erroRotulo = 'Z'; //FIXME: não deveria ser um rótulo fixo.
      ptosControle[0] = new Ponto(this, ref erroRotulo, new Ponto4D(0.5f, -0.5f));  // ptoA
      FilhoAdicionar(ptosControle[0]);
      ptosControle[0].ObjetoAtualizar();
      ptosControle[1] = new Ponto(this, ref erroRotulo, new Ponto4D(0.5f, 0.5f));  // ptoB
      FilhoAdicionar(ptosControle[1]);
      ptosControle[1].ObjetoAtualizar();
      ptosControle[2] = new Ponto(this, ref erroRotulo, new Ponto4D(-0.5f, 0.5f));  // ptoC
      FilhoAdicionar(ptosControle[2]);
      ptosControle[2].ObjetoAtualizar();
      ptosControle[3] = new Ponto(this, ref erroRotulo, new Ponto4D(-0.5f, -0.5f));  // ptoD
      FilhoAdicionar(ptosControle[3]);
      ptosControle[3].ObjetoAtualizar();
    }

    private void PoliedroControle()
    {
      char erroRotulo = 'Z'; //FIXME: não deveria ser um rótulo fixo.
      List<Ponto4D> pontos = new List<Ponto4D>();
      pontos.Add(ptosControle[0].PontosId(0));
      pontos.Add(ptosControle[1].PontosId(0));
      pontos.Add(ptosControle[2].PontosId(0));
      pontos.Add(ptosControle[3].PontosId(0));
      poliedroControle = new Poligono(this, ref erroRotulo, pontos);
      poliedroControle.PrimitivaTipo = PrimitiveType.LineStrip;
      FilhoAdicionar(poliedroControle);
      poliedroControle.ObjetoAtualizar();
    }

    public void Atualizar()
    {
      Ponto4D ptoA = new Ponto4D(ptosControle[0].PontosId(0));
      Ponto4D ptoB = new Ponto4D(ptosControle[1].PontosId(0));
      Ponto4D ptoC = new Ponto4D(ptosControle[2].PontosId(0));
      Ponto4D ptoD = new Ponto4D(ptosControle[3].PontosId(0));
      double valorX = 0, valorY = 0;
      for (int i = 0; i <= splineQtdPto; i++)
      {
        valorX = ptoA.X * bezierMatrizPeso[i, 0] + ptoB.X * bezierMatrizPeso[i, 1] + ptoC.X * bezierMatrizPeso[i, 2] + ptoD.X * bezierMatrizPeso[i, 3];
        valorY = ptoA.Y * bezierMatrizPeso[i, 0] + ptoB.Y * bezierMatrizPeso[i, 1] + ptoC.Y * bezierMatrizPeso[i, 2] + ptoD.Y * bezierMatrizPeso[i, 3];
        PontosAlterar(new Ponto4D(valorX, valorY), i);
      }

      base.ObjetoAtualizar();
    }

    public void AtualizarSpline(Ponto4D ptoInc, bool proximo = false)
    {
      if (proximo)
      {
        ptoSelecionado = (ptoSelecionado >= 3) ? 0 : ptoSelecionado += 1;
      }

      ptosControle[ptoSelecionado].PontosAlterar(ptosControle[ptoSelecionado].PontosId(0) + ptoInc, 0);
      ptosControle[ptoSelecionado].ObjetoAtualizar();

      poliedroControle.PontosAlterar(ptosControle[ptoSelecionado].PontosId(0), ptoSelecionado);
      poliedroControle.ObjetoAtualizar();

      Atualizar();
    }

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Spline _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);

    }
#endif

  }
}
