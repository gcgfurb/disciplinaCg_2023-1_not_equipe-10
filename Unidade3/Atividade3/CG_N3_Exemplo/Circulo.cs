#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Circulo : Objeto
  {
    private double raio;

    public Circulo(Objeto paiRef, ref char _rotulo) : 
      this (paiRef, ref _rotulo,0.1,new Ponto4D()) {}

    public Circulo(Objeto paiRef, ref char _rotulo, double _raio, Ponto4D ptoDeslocamento) : base(paiRef,ref _rotulo)
    {
      raio = _raio;
      PrimitivaTipo = PrimitiveType.Points;
      PrimitivaTamanho = 5;
      for (int angulo = 0; angulo < 360; angulo += 5)
      {
        base.PontosAdicionar(Matematica.GerarPtosCirculo(angulo, raio) + ptoDeslocamento);
      }

      base.ObjetoAtualizar();
    }

    public void Atualizar(Ponto4D ptoDeslocamento)
    {
      int id = 0;
      for (int angulo = 0; angulo < 360; angulo += 5)
      {
        base.PontosAlterar(Matematica.GerarPtosCirculo(angulo, raio) + ptoDeslocamento, id);
        id++;
      }

      base.ObjetoAtualizar();
    }

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Circulo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);

    }
#endif

  }
}
