#define CG_OpenGL
#define CG_Debug
// #define CG_DirectX

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace gcgcg
{
  internal class Objeto
  {
    // Objeto
    protected char rotulo;
    public char Rotulo { get => rotulo; set => rotulo = value; }
    protected Objeto paiRef;
    // public Objeto PaiRef { get => paiRef; }
    private List<Objeto> objetosLista = new List<Objeto>();
    private PrimitiveType primitivaTipo = PrimitiveType.LineLoop;
    public PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    private float primitivaTamanho = 1;
    public float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }
    private Shader _shaderCor = new Shader("Shaders/shader.vert", "Shaders/shaderBranca.frag");
    public Shader shaderCor { set => _shaderCor = value; }

    // Vértices do objeto
    protected List<Ponto4D> pontosLista = new List<Ponto4D>();
    private int _vertexBufferObject;
    private int _vertexArrayObject;

    public Objeto(Objeto paiRef)
    {
      this.rotulo = '@';
      this.paiRef = paiRef;
    }

    public void ObjetoAtualizar()
    {
      float[] vertices = new float[pontosLista.Count * 3];
      int ptoLista = 0;
      for (int i = 0; i < vertices.Length; i += 3)
      {
        vertices[i] = (float)pontosLista[ptoLista].X;
        vertices[i + 1] = (float)pontosLista[ptoLista].Y;
        vertices[i + 2] = (float)pontosLista[ptoLista].Z;
        ptoLista++;
      }

      _vertexBufferObject = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
      _vertexArrayObject = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
    }

    public void Desenhar()
    {
#if CG_OpenGL && !CG_DirectX
      GL.PointSize(primitivaTamanho);
      GL.BindVertexArray(_vertexArrayObject);
      _shaderCor.Use();
      GL.DrawArrays(primitivaTipo, 0, pontosLista.Count);
#elif CG_DirectX && !CG_OpenGL
      Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
      Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }
    }

    public void FilhoAdicionar(Objeto filho)
    {
      this.objetosLista.Add(filho);
    }

    // public void FilhoRemover(Objeto filho)
    // {
    //   this.objetosLista.Remove(filho);
    // }

    // public void GrafocenaRemover()
    // {
    //   for (var i = 0; i < objetosLista.Count; i++)
    //   {
    //     objetosLista[i].GrafocenaRemover();
    //   }
    //   objetosLista.Clear();
    // }

    // public void GrafocenaToString()
    // {
    //   Console.WriteLine(this);
    //   for (var i = 0; i < objetosLista.Count; i++)
    //   {
    //     Console.WriteLine(objetosLista[i]);
    //   }
    // }

    // public bool GrafocenaRemoverPoligonoVazio()
    // {
    //   for (var i = 0; i < objetosLista.Count; i++)
    //   {
    //     if (objetosLista[i].GrafocenaRemoverPoligonoVazio())
    //     {
    //       objetosLista.RemoveAt(i);
    //     }
    //   }
    //   if (PontosVazio())
    //   {
    //     return true;
    //   }
    //   return false;
    // }

    // public void PrimitivaTipoTroca()
    // {
    //   if (primitivaTipo == PrimitiveType.LineLoop)
    //     primitivaTipo = PrimitiveType.LineStrip;
    //   else
    //     primitivaTipo = PrimitiveType.LineLoop;
    // }

    public void PontosAdicionar(Ponto4D pto)
    {
      pontosLista.Add(pto);
    }

    // public void PontosRemoverUltimo()
    // {
    //   pontosLista.RemoveAt(pontosLista.Count - 1);
    // }

    // protected bool PontosVazio()
    // {
    //   if (pontosLista.Count == 0)
    //   {
    //     return true;
    //   }
    //   return false;
    // }

    public Ponto4D PontosId(int id)
    {
      return pontosLista[id];
    }

    // public Ponto4D PontosUltimo()
    // {
    //   return pontosLista[pontosLista.Count - 1];
    // }

    public void PontosAlterar(Ponto4D pto, int posicao)
    {
      pontosLista[posicao] = pto;
    }

    public void OnUnload()
    {
      foreach (var objeto in objetosLista)
      {
        objeto.OnUnload();
      }

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);
      GL.UseProgram(0);

      GL.DeleteBuffer(_vertexBufferObject);
      GL.DeleteVertexArray(_vertexArrayObject);

      GL.DeleteProgram(_shaderCor.Handle);
    }

#if CG_Debug
    protected string ImprimeToString()
    {
      string retorno;
      retorno = "__ Objeto: " + rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[ " +
        string.Format("{0,10}", pontosLista[i].X) + " | " +
        string.Format("{0,10}", pontosLista[i].Y) + " | " +
        string.Format("{0,10}", pontosLista[i].Z) + " | " +
        string.Format("{0,10}", pontosLista[i].W) + " ]" + "\n";
      }
      return (retorno);
    }
#endif

  }
}