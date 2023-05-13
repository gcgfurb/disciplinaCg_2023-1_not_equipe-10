#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class SrPalito : Objeto
  {
    private Ponto4D ptoPe = new Ponto4D();
    private Ponto4D ptoCabeca = new Ponto4D();
    private double angulo = 45;
    private double raio = 0.5;

    public SrPalito(Objeto paiRef, ref char _rotulo) : base(paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.Lines;
      PrimitivaTamanho = 1;

      base.PontosAdicionar(ptoPe);
      base.PontosAdicionar(ptoCabeca);
      Atualizar();
    }

    public void Atualizar()
    {
      base.PontosAlterar(ptoPe, 0);
      ptoCabeca = Matematica.GerarPtosCirculo(angulo, raio);
      ptoCabeca.X += ptoPe.X;
      PontosAlterar(ptoCabeca, 1);
      base.ObjetoAtualizar();
    }

    public void AtualizarPe(double peInc)
    {
      ptoPe = new Ponto4D(ptoPe.X + peInc, ptoPe.Y);
      Atualizar();
    }

    public void AtualizarRaio(double raioInc)
    {
      raio += raioInc;
      Atualizar();
    }

    public void AtualizarAngulo(double anguloInc)
    {
      angulo += anguloInc;
      Atualizar();
    }

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto SrPalito _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);
    }
#endif

  }
}
