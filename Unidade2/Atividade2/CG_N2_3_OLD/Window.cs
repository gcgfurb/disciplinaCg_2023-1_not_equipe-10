﻿//#define CG_Privado // código do professor.
#define CG_Gizmo  // debugar gráfico.
#define CG_Debug // debugar texto.
#define CG_OpenGL // render OpenGL.
//#define CG_DirectX // render DirectX.
//#define CG_Privado // código do professor.

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Threading;

namespace gcgcg
{
  public class Window : GameWindow
  {
    private readonly float[] _sruEixos =
    {
       0.5f,  0.0f,  0.0f, // X+
       0.0f,  0.0f,  0.0f, // X-
       0.0f,  0.5f,  0.0f, // Y+
       0.0f,  0.0f,  0.0f, // Y-
       0.0f,  0.0f,  0.5f, // Z+
       0.0f,  0.0f, -0.5f, // Z-
    };

    private int _vertexBufferObject_sruEixos;
    private int _vertexArrayObject_sruEixos;

    private Shader _shaderVermelho;
    private Shader _shaderVerde;
    private Shader _shaderAzul;

    private List<Objeto> objetosLista = new List<Objeto>();
    private Objeto objetoSelecionado = null;
    private char objetoId = '@';
    private Objeto objetoNovo = null;
    private double linhaY = 0;
    private double linhaX = 0;
    private double linhaTamanho = 0.5;
    private double linhaAngulo = 45;

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
      base.OnLoad();

      Redesenhar();
    }

    public void Redesenhar() {
      GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
      GL.Clear(ClearBufferMask.ColorBufferBit);
      
      // Eixos
      _vertexBufferObject_sruEixos = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_sruEixos);
      GL.BufferData(BufferTarget.ArrayBuffer, _sruEixos.Length * sizeof(float), _sruEixos, BufferUsageHint.StaticDraw);
      _vertexArrayObject_sruEixos = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      _shaderVermelho = new Shader("Shaders/shader.vert", "Shaders/shaderVermelho.frag");
      _shaderVerde = new Shader("Shaders/shader.vert", "Shaders/shaderVerde.frag");
      _shaderAzul = new Shader("Shaders/shader.vert", "Shaders/shaderAzul.frag");
      
      objetosLista.Clear();
      objetoNovo = new Objeto(objetoId, null, PrimitiveType.Lines);
      objetosLista.Add(objetoNovo);
      objetoNovo.PontosAdicionar(new Ponto4D(linhaX, linhaY));
      var segundoPonto = Matematica.GerarPtosCirculo(linhaAngulo, linhaTamanho);
      segundoPonto.X += linhaX;
      objetoNovo.PontosAdicionar(segundoPonto);
      
      objetoNovo.Atualizar();
      objetoSelecionado = objetoNovo;
      objetoNovo = null;
      SwapBuffers();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit);

#if CG_Gizmo      
      Sru3D();
#endif
      for (var i = 0; i < objetosLista.Count; i++)
        objetosLista[i].Desenhar();

      SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      var input = KeyboardState;

      if (input.IsKeyDown(Keys.Escape))
      {
        Close();
      } else if (input.IsKeyDown(Keys.Q))
      {
        linhaX -= 0.01;
        Thread.Sleep(50);
        Redesenhar();
      } else if (input.IsKeyDown(Keys.E))
      {
        linhaX += 0.01;
        Thread.Sleep(50);
        Redesenhar();
      } else if (input.IsKeyDown(Keys.A))
      {
        linhaTamanho -= 0.01;
        Thread.Sleep(50);
        Redesenhar();
      } else if (input.IsKeyDown(Keys.S))
      {
        linhaTamanho += 0.01;
        Thread.Sleep(50);
        Redesenhar();
      } else if (input.IsKeyDown(Keys.Z))
      {
        linhaAngulo -= 1;
        Thread.Sleep(50);
        Redesenhar();
      } else if (input.IsKeyDown(Keys.X))
      {
        linhaAngulo += 1;
        Thread.Sleep(50);
        Redesenhar();
      }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(0, 0, Size.X, Size.Y);
    }

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

      base.OnUnload();
    }

#if CG_Gizmo
    private void Sru3D()
    {
#if CG_OpenGL
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      // EixoX
      _shaderVermelho.Use();
      GL.DrawArrays(PrimitiveType.Lines, 0, 2);
      // EixoY
      _shaderVerde.Use();
      GL.DrawArrays(PrimitiveType.Lines, 2, 2);
      // EixoZ
      _shaderAzul.Use();
      GL.DrawArrays(PrimitiveType.Lines, 4, 2);
#endif
    }
#endif    

  }
}
