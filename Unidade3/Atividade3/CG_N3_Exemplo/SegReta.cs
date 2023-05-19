#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class SegReta : Objeto
  {
    public SegReta(Objeto paiRef, ref char _rotulo) : 
      this(paiRef, ref _rotulo, new Ponto4D(0.5,0.5), new Ponto4D(-0.5,-0.5)) {}

    public SegReta(Objeto paiRef, ref char _rotulo, Ponto4D ptoIni, Ponto4D ptoFim) : base(paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.Lines;
      PrimitivaTamanho = 1;

      base.PontosAdicionar(ptoIni);
      base.PontosAdicionar(ptoFim);
      Atualizar();
    }

    private void Atualizar()
    {

      base.ObjetoAtualizar();
    }

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto SegReta _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);
    }
#endif

  }
}
