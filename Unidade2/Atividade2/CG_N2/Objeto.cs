/**
  Autor: Dalton Solano dos Reis
**/

#define CG_OpenGL
#define CG_Debug
// #define CG_DirectX

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System;

namespace gcgcg
{
  internal class Objeto
  {
    protected List<Ponto4D> pontosLista = new List<Ponto4D>();

    protected char rotulo;
    protected Objeto paiRef;
    public Objeto PaiRef { get => paiRef; }
    private PrimitiveType primitivaTipo = PrimitiveType.LineLoop;
    public PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    private List<Objeto> objetosLista = new List<Objeto>();

    private int _vertexBufferObject_sruEixos;
    private int _vertexArrayObject_sruEixos;
    private Shader _shaderBranco;

    public Objeto(char rotulo, Objeto paiRef)
    {
      this.rotulo = rotulo;
      this.paiRef = paiRef;
    }

    public void Atualizar()
    {
      float[] vertices = new float[pontosLista.Count * 3];
      int ptoLista = 0;
      for (int i = 0; i < vertices.Length; i+=3)
      {
        vertices[i] = (float) pontosLista[ptoLista].X;
        vertices[i+1] = (float) pontosLista[ptoLista].Y;
        vertices[i+2] = (float) pontosLista[ptoLista].Z;
        ptoLista++;
      }

      _vertexBufferObject_sruEixos = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_sruEixos);
      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
      _vertexArrayObject_sruEixos = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      _shaderBranco = new Shader("Shaders/shader.vert", "Shaders/shaderBranco.frag");
    }

    public void Desenhar()
    {
#if CG_OpenGL && !CG_DirectX
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      _shaderBranco.Use();
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

    public void FilhoRemover(Objeto filho)
    {
      this.objetosLista.Remove(filho);
    }

    public void GrafocenaRemover()
    {
      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].GrafocenaRemover();
      }
      objetosLista.Clear();
    }

    public void GrafocenaToString()
    {
      Console.WriteLine(this);
      for (var i = 0; i < objetosLista.Count; i++)
      {
        Console.WriteLine(objetosLista[i]);
      }
    }

    public bool GrafocenaRemoverPoligonoVazio()
    {
      for (var i = 0; i < objetosLista.Count; i++)
      {
        if (objetosLista[i].GrafocenaRemoverPoligonoVazio())
        {
          objetosLista.RemoveAt(i);
        }
      }
      if (PontosVazio())
      {
        return true;
      }
      return false;
    }

    public void PrimitivaTipoTroca()
    {
      if (primitivaTipo == PrimitiveType.LineLoop)
        primitivaTipo = PrimitiveType.LineStrip;
      else
        primitivaTipo = PrimitiveType.LineLoop;
    }

    public void PontosAdicionar(Ponto4D pto)
    {
      pontosLista.Add(pto);
    }

    public void PontosRemoverUltimo()
    {
      pontosLista.RemoveAt(pontosLista.Count - 1);
    }

    protected bool PontosVazio()
    {
      if (pontosLista.Count == 0)
      {
        return true;
      }
      return false;
    }

    public Ponto4D PontosUltimo()
    {
      return pontosLista[pontosLista.Count - 1];
    }

    /* [FIXME: destroctor]    
        protected override void OnUnload()
        {
          GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
          GL.BindVertexArray(0);
          GL.UseProgram(0);

          GL.DeleteBuffer(_vertexBufferObject_sruEixos);
          GL.DeleteVertexArray(_vertexArrayObject_sruEixos);

          GL.DeleteProgram(_shaderVermelho.Handle);
          GL.DeleteProgram(_shaderVerde.Handle);
          GL.DeleteProgram(_shaderAzul.Handle);
          GL.DeleteProgram(_shaderBranco.Handle);

          base.OnUnload();
        }
    */

    //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto: " + rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }
#endif

  }
}