#define CG_Gizmo  // debugar gráfico.
#define CG_OpenGL // render OpenGL.
// #define CG_DirectX // render DirectX.
#define CG_Privado // código do professor.

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using System.Collections.Generic;

//FIXME: padrão Singleton

namespace gcgcg
{
  public class Mundo : GameWindow
  {
    Objeto mundo;
    private char rotuloAtual = '?';
    private Objeto objetoSelecionado = null;

    private readonly float[] _sruEixos =
    {
      -0.5f,  0.0f,  0.0f, /* X- */      0.5f,  0.0f,  0.0f, /* X+ */
       0.0f, -0.5f,  0.0f, /* Y- */      0.0f,  0.5f,  0.0f, /* Y+ */
       0.0f,  0.0f, -0.5f, /* Z- */      0.0f,  0.0f,  0.5f, /* Z+ */
    };

    private int _vertexBufferObject_sruEixos;
    private int _vertexArrayObject_sruEixos;

    private Shader _shaderVermelha;
    private Shader _shaderVerde;
    private Shader _shaderAzul;

    public Mundo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
    {
      mundo = new Objeto(null, ref rotuloAtual);
    }

    private void Diretivas()
    {
#if DEBUG
      Console.WriteLine("Debug version");
#endif      
#if RELEASE
    Console.WriteLine("Release version");
#endif      
#if CG_Gizmo      
      Console.WriteLine("#define CG_Gizmo  // debugar gráfico.");
#endif
#if CG_OpenGL      
      Console.WriteLine("#define CG_OpenGL // render OpenGL.");
#endif
#if CG_DirectX      
      Console.WriteLine("#define CG_DirectX // render DirectX.");
#endif
#if CG_Privado      
      Console.WriteLine("#define CG_Privado // código do professor.");
#endif
      Console.WriteLine("__________________________________ \n");
    }

    protected override void OnLoad()
    {
      base.OnLoad();

      Diretivas();

      GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

      #region Eixos: SRU  
      _vertexBufferObject_sruEixos = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_sruEixos);
      GL.BufferData(BufferTarget.ArrayBuffer, _sruEixos.Length * sizeof(float), _sruEixos, BufferUsageHint.StaticDraw);
      _vertexArrayObject_sruEixos = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      _shaderVermelha = new Shader("Shaders/shader.vert", "Shaders/shaderVermelha.frag");
      _shaderVerde = new Shader("Shaders/shader.vert", "Shaders/shaderVerde.frag");
      _shaderAzul = new Shader("Shaders/shader.vert", "Shaders/shaderAzul.frag");
      #endregion

      // #region Objeto: polígono qualquer  
      List<Ponto4D> pontosPoligono = new List<Ponto4D>();
      pontosPoligono.Add(new Ponto4D(0.25, 0.25));
      pontosPoligono.Add(new Ponto4D(0.75, 0.25));
      pontosPoligono.Add(new Ponto4D(0.75, 0.75));
      pontosPoligono.Add(new Ponto4D(0.50, 0.50));
      pontosPoligono.Add(new Ponto4D(0.25, 0.75));
      objetoSelecionado = new Poligono(mundo, ref rotuloAtual, pontosPoligono);
      // #endregion
      // #region NÃO USAR: declara um objeto filho ao polígono
      // objetoSelecionado = new Ponto(objetoSelecionado, ref rotuloAtual, new Ponto4D(0.50, 0.75));
      // objetoSelecionado.ToString();
      // #endregion

      // #region Objeto: retângulo  
      // objetoSelecionado = new Retangulo(mundo, ref rotuloAtual, new Ponto4D(-0.25, 0.25), new Ponto4D(-0.75, 0.75));
      // objetoSelecionado.PrimitivaTipo = PrimitiveType.LineLoop;
      // #endregion

      #region Objeto: segmento de reta  
      objetoSelecionado = new SegReta(mundo, ref rotuloAtual, new Ponto4D(-0.5, -0.5), new Ponto4D());
      #endregion

      // #region Objeto: ponto  
      // objetoSelecionado = new Ponto(mundo, ref rotuloAtual, new Ponto4D(-0.25, -0.25));
      // objetoSelecionado.PrimitivaTipo = PrimitiveType.Points;
      // objetoSelecionado.PrimitivaTamanho = 5; // FIXME: não está mudando o tamanho
      // #endregion

#if CG_Privado
      // #region Objeto: circulo  
      // objetoSelecionado = new Circulo(mundo, ref rotuloAtual, 0.2, new Ponto4D());
      // objetoSelecionado.shaderCor = new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag");
      // #endregion

      // #region Objeto: SrPalito  
      // objetoSelecionado = new SrPalito(mundo, ref rotuloAtual);
      // #endregion

      // #region Objeto: Spline
      // objetoSelecionado = new Spline(mundo, ref rotuloAtual);
      // #endregion
#endif

    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);

#if CG_Gizmo      
      Sru3D();
#endif
      mundo.Desenhar();
      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      // ☞ 396c2670-8ce0-4aff-86da-0f58cd8dcfdc
      #region Teclado
      var input = KeyboardState;
      if (input.IsKeyDown(Keys.Escape))
      {
        Close();
      }
      else
      {
        if (input.IsKeyPressed(Keys.G))
        {
          mundo.GrafocenaImprimir("");
        }
        else
        {
          if (input.IsKeyPressed(Keys.P))
          {
            System.Console.WriteLine(objetoSelecionado.ToString());
          }
          else
          {
            if (input.IsKeyPressed(Keys.M))
              objetoSelecionado.MatrizImprimir();
            else
            {
              //TODO: não está atualizando a BBox com as transformações geométricas
              if (input.IsKeyPressed(Keys.I))
                objetoSelecionado.MatrizAtribuirIdentidade();
              else
              {
                if (input.IsKeyPressed(Keys.Left))
                  objetoSelecionado.MatrizTranslacaoXYZ(-0.05, 0, 0);
                else
                {
                  if (input.IsKeyPressed(Keys.Right))
                    objetoSelecionado.MatrizTranslacaoXYZ(0.05, 0, 0);
                  else
                  {
                    if (input.IsKeyPressed(Keys.Up))
                      objetoSelecionado.MatrizTranslacaoXYZ(0, 0.05, 0);
                    else
                    {
                      if (input.IsKeyPressed(Keys.Down))
                        objetoSelecionado.MatrizTranslacaoXYZ(0, -0.05, 0);
                      else
                      {
                        if (input.IsKeyPressed(Keys.PageUp))
                          objetoSelecionado.MatrizEscalaXYZ(2, 2, 2);
                        else
                        {
                          if (input.IsKeyPressed(Keys.PageDown))
                            objetoSelecionado.MatrizEscalaXYZ(0.5, 0.5, 0.5);
                          else
                          {
                            if (input.IsKeyPressed(Keys.Home))
                              objetoSelecionado.MatrizEscalaXYZBBox(0.5, 0.5, 0.5);
                            else
                            {
                              if (input.IsKeyPressed(Keys.End))
                                objetoSelecionado.MatrizEscalaXYZBBox(2, 2, 2);
                              else
                              {
                                if (input.IsKeyPressed(Keys.D1))
                                  objetoSelecionado.MatrizRotacao(10);
                                else
                                {
                                  if (input.IsKeyPressed(Keys.D2))
                                    objetoSelecionado.MatrizRotacao(-10);
                                  else
                                  {
                                    if (input.IsKeyPressed(Keys.D3))
                                      objetoSelecionado.MatrizRotacaoZBBox(10);
                                    else
                                    {
                                      if (input.IsKeyPressed(Keys.D4))
                                        objetoSelecionado.MatrizRotacaoZBBox(-10);
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      #endregion

      #region  Mouse
      // ☞ cc6efca2-aba0-4a49-b49e-d8e937028d26
      #endregion

    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(0, 0, Size.X, Size.Y);
    }

    protected override void OnUnload()
    {
      mundo.OnUnload();

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);
      GL.UseProgram(0);

      GL.DeleteBuffer(_vertexBufferObject_sruEixos);
      GL.DeleteVertexArray(_vertexArrayObject_sruEixos);

      GL.DeleteProgram(_shaderVermelha.Handle);
      GL.DeleteProgram(_shaderVerde.Handle);
      GL.DeleteProgram(_shaderAzul.Handle);

      base.OnUnload();
    }

#if CG_Gizmo
    private void Sru3D()
    {
#if CG_OpenGL && !CG_DirectX
      var transform = Matrix4.Identity;
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      // EixoX
      _shaderVermelha.SetMatrix4("transform", transform);
      _shaderVermelha.Use();
      GL.DrawArrays(PrimitiveType.Lines, 0, 2);
      // EixoY
      _shaderVerde.SetMatrix4("transform", transform);
      _shaderVerde.Use();
      GL.DrawArrays(PrimitiveType.Lines, 2, 2);
      // EixoZ
      _shaderAzul.SetMatrix4("transform", transform);
      _shaderAzul.Use();
      GL.DrawArrays(PrimitiveType.Lines, 4, 2);
#elif CG_DirectX && !CG_OpenGL
      Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
      Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
    }
#endif    

  }
}
