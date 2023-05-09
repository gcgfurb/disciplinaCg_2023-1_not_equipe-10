#define CG_Gizmo  // debugar gráfico.
#define CG_OpenGL // render OpenGL.
//#define CG_DirectX // render DirectX.
// #define CG_Privado // código do professor.

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using OpenTK.Mathematics;
using System.Linq;

//FIXME: padrão Singleton

namespace gcgcg
{
    public class Mundo : GameWindow
    {
        private List<Objeto> objetosLista = new List<Objeto>();
        private List<Objeto> pontosControle = new List<Objeto>();
        private int _ind = 0;
        private Objeto objetoSelecionado = null;
        private char rotulo = '@';
        // private Ponto pontoA;
        // private Ponto pontoB;
        // private Ponto pontoC;
        // private Ponto pontoD;

        private Ponto4D pontoA;
        private Ponto4D pontoB;
        private Ponto4D pontoC;
        private Ponto4D pontoD;
        // private Ponto4D pontoE;

        private readonly float[] _sruEixos =
        {
       0.0f,  0.0f,  0.0f, /* X- */      0.5f,  0.0f,  0.0f, /* X+ */
       0.0f,  0.0f,  0.0f, /* Y- */      0.0f,  0.5f,  0.0f, /* Y+ */
       0.0f,  0.0f, -0.5f, /* Z- */      0.0f,  0.0f,  0.5f, /* Z+ */
    };

        private int _vertexBufferObject_sruEixos;
        private int _vertexArrayObject_sruEixos;

        private Shader _shaderVermelha;
        private Shader _shaderVerde;
        private Shader _shaderAzul;

        private bool _firstMove = true;
        private Vector2 _lastPos;
        private int qntdPontos = 10;

        public Mundo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
               : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        private void ObjetoNovo(Objeto objeto, Objeto objetoFilho = null)
        {
            if (objetoFilho == null)
            {
                objetosLista.Add(objeto);
                objeto.Rotulo = rotulo = Utilitario.charProximo(rotulo);
                objeto.ObjetoAtualizar();
                objetoSelecionado = objeto;
            }
            else
            {
                objeto.FilhoAdicionar(objetoFilho);
                objetoFilho.Rotulo = rotulo = Utilitario.charProximo(rotulo);
                objetoFilho.ObjetoAtualizar();
                objetoSelecionado = objetoFilho;
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            // Eixos
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

            CriarPontosControle();

            Redesenhar();
        }

        public void CriarPontosControle()
        {
            pontosControle.Clear();
            objetosLista.Clear();

            pontoA = new Ponto4D(-0.5, -0.5);
            pontoB = new Ponto4D(-0.5, 0.5);
            pontoC = new Ponto4D(0.5, 0.5);
            pontoD = new Ponto4D(0.5, -0.5);

            ObjetoNovo(new Ponto(null, pontoA));
            ObjetoNovo(new Ponto(null, pontoB));
            ObjetoNovo(new Ponto(null, pontoC));
            ObjetoNovo(new Ponto(null, pontoD));

            pontosControle.AddRange(objetosLista.Take(4));
        }

        public void Redesenhar()
        {
            objetosLista.Clear();
            objetosLista.AddRange(pontosControle);
            Objeto objetoNovo = null;

            ObjetoNovo(new SegReta(null, objetosLista[0].PontosId(0), objetosLista[1].PontosId(0)));
            ObjetoNovo(new SegReta(null, objetosLista[1].PontosId(0), objetosLista[2].PontosId(0)));
            ObjetoNovo(new SegReta(null, objetosLista[2].PontosId(0), objetosLista[3].PontosId(0)));


            var spline = new Spline(
                objetoNovo, 
                objetosLista[0].PontosId(0), 
                objetosLista[1].PontosId(0), 
                objetosLista[2].PontosId(0), 
                objetosLista[3].PontosId(0), 
                qntdPontos);
            foreach (var item in spline.resultados)
            {
                int index = spline.resultados.IndexOf(item);
                if (index < spline.resultados.Count - 1)
                {
                    ObjetoNovo(new SegReta(null, item, spline.resultados[index + 1]));
                }
            }

            objetoNovo = new SegReta(null, spline.resultados.LastOrDefault(), objetosLista[3].PontosId(0));
            ObjetoNovo(objetoNovo); objetoNovo = null;
            objetoSelecionado = objetosLista[_ind];
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

            #region Teclado
            var input = KeyboardState;
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            else if (input.IsKeyDown(Keys.C))
            {
                objetoSelecionado.PontosAlterar(new Ponto4D(objetoSelecionado.PontosId(0).X, objetoSelecionado.PontosId(0).Y + 0.005, 0), 0);
                objetoSelecionado.ObjetoAtualizar();
                Redesenhar();
            }
            else if (input.IsKeyDown(Keys.B))
            {
                objetoSelecionado.PontosAlterar(new Ponto4D(objetoSelecionado.PontosId(0).X, objetoSelecionado.PontosId(0).Y - 0.005, 0), 0);
                objetoSelecionado.ObjetoAtualizar();
                Redesenhar();
            }
            else if (input.IsKeyDown(Keys.E))
            {
                objetoSelecionado.PontosAlterar(new Ponto4D(objetoSelecionado.PontosId(0).X - 0.005, objetoSelecionado.PontosId(0).Y, 0), 0);
                objetoSelecionado.ObjetoAtualizar();
                Redesenhar();
            }
            else if (input.IsKeyDown(Keys.D))
            {
                objetoSelecionado.PontosAlterar(new Ponto4D(objetoSelecionado.PontosId(0).X + 0.005, objetoSelecionado.PontosId(0).Y, 0), 0);
                objetoSelecionado.ObjetoAtualizar();
                Redesenhar();
            }
            else if (input.IsKeyPressed(Keys.Equal))
            {
                qntdPontos++;
                Redesenhar();
            }
            else if (input.IsKeyPressed(Keys.Minus))
            {
                if(qntdPontos > 1) qntdPontos--;
                Redesenhar();
            }
            else if (input.IsKeyPressed(Keys.R))
            {
                CriarPontosControle();
                Redesenhar();
            }
            else if (input.IsKeyPressed(Keys.Space))
            {
                if (objetoSelecionado == null)
                    Console.WriteLine("objetoSelecionado: NULL!");
                else if (objetosLista.Count == 0)
                    Console.WriteLine("objetoLista: vazia!");
                else
                {
                    int ind = 0;
                    foreach (var objetoNovo in objetosLista)
                    {
                        objetoNovo.shaderPonto = new Shader("Shaders/shader.vert", "Shaders/shaderCiano.frag");
                        if (objetoNovo == objetoSelecionado)
                        {
                            ind++;
                            if (ind >= 4)
                                ind = 0;
                            break;
                        }
                        ind++;
                    }
                    _ind = ind;
                    objetoSelecionado = objetosLista[ind];
                    objetoSelecionado.shaderPonto = new Shader("Shaders/shader.vert", "Shaders/shaderVermelha.frag");
                }
            }
            #endregion

            #region  Mouse
            var mouse = MouseState;
            // Mouse FIXME: inverte eixo Y, fazer NDC para proporção em tela
            Vector2i janela = this.ClientRectangle.Size;

            if (input.IsKeyDown(Keys.LeftShift))
            {
                if (_firstMove)
                {
                    _lastPos = new Vector2(mouse.X, mouse.Y);
                    _firstMove = false;
                }
                else
                {
                    var deltaX = (mouse.X - _lastPos.X) / janela.X;
                    var deltaY = (mouse.Y - _lastPos.Y) / janela.Y;
                    _lastPos = new Vector2(mouse.X, mouse.Y);

                    objetoSelecionado.PontosAlterar(new Ponto4D(objetoSelecionado.PontosId(0).X + deltaX, objetoSelecionado.PontosId(0).Y + deltaY, 0), 0);
                    objetoSelecionado.ObjetoAtualizar();
                }
            }
            if (input.IsKeyDown(Keys.RightShift))
            {
                objetoSelecionado.PontosAlterar(new Ponto4D(mouse.X / janela.X, mouse.Y / janela.Y, 0), 0);
                objetoSelecionado.ObjetoAtualizar();
            }
            #endregion

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            foreach (var objeto in objetosLista)
            {
                objeto.OnUnload();
            }

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
            GL.BindVertexArray(_vertexArrayObject_sruEixos);
            // EixoX
            _shaderVermelha.Use();
            GL.DrawArrays(PrimitiveType.Lines, 0, 2);
            // EixoY
            _shaderVerde.Use();
            GL.DrawArrays(PrimitiveType.Lines, 2, 2);
            // EixoZ
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
