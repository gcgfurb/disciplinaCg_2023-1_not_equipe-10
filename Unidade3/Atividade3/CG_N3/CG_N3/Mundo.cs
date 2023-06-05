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

        public static Funcao funcaoAtiva = Funcao.DEFAULT;
        private int indiceVerticeSelecionado = -1;
        private bool desenharBbox = false;

        private readonly float[] _sruEixos =
        {
      -0.5f,  0.0f,  0.0f, /* X- */      0.5f,  0.0f,  0.0f, /* X+ */
       0.0f, -0.5f,  0.0f, /* Y- */      0.0f,  0.5f,  0.0f, /* Y+ */
       0.0f,  0.0f, -0.5f, /* Z- */      0.0f,  0.0f,  0.5f, /* Z+ */
    };

        private readonly float[] _bboxTeste =
        {
           0.25f, 0.25f,  0.0f, /* X- */      0.75f, 0.25f,  0.0f, /* Y- */
           0.75f,  0.75f,  0.0f, /* X+ */     0.25f,  0.75f,  0.0f, /* Y+ */
        };

        private int _vertexBufferObject_sruEixos;
        private int _vertexArrayObject_sruEixos;

        private Shader _shaderVermelha;
        private Shader _shaderVerde;
        private Shader _shaderAzul;
        private Shader _shaderAmarela;


        private int _vertexBufferObject;
        private int _vertexArrayObject;

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
            _shaderAmarela = new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag");
            #endregion



            #region     ATIVIDADE 2

            List<Ponto4D> pontosPoligono = new List<Ponto4D>();
            pontosPoligono.Add(new Ponto4D(0.25, 0.25));
            pontosPoligono.Add(new Ponto4D(0.75, 0.25));
            pontosPoligono.Add(new Ponto4D(0.75, 0.75));
            pontosPoligono.Add(new Ponto4D(0.50, 0.50));
            pontosPoligono.Add(new Ponto4D(0.25, 0.75));
            objetoSelecionado = new Poligono(mundo, ref rotuloAtual, pontosPoligono);

            List<Ponto4D> pontosPoligono2 = new List<Ponto4D>();
            pontosPoligono2.Add(new Ponto4D(-0.25, -0.25));
            pontosPoligono2.Add(new Ponto4D(-0.75, -0.25));
            pontosPoligono2.Add(new Ponto4D(-0.75, -0.75));
            pontosPoligono2.Add(new Ponto4D(-0.50, -0.50));
            pontosPoligono2.Add(new Ponto4D(-0.25, -0.75));
            objetoSelecionado = new Poligono(mundo, ref rotuloAtual, pontosPoligono2);

            #endregion


            // #region NÃO USAR: declara um objeto filho ao polígono
            // objetoSelecionado = new Ponto(objetoSelecionado, ref rotuloAtual, new Ponto4D(0.50, 0.75));
            // objetoSelecionado.ToString();
            // #endregion

            // #region Objeto: retângulo  
            // objetoSelecionado = new Retangulo(mundo, ref rotuloAtual, new Ponto4D(-0.25, 0.25), new Ponto4D(-0.75, 0.75));
            // objetoSelecionado.PrimitivaTipo = PrimitiveType.LineLoop;
            // #endregion

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

            var mouse = MouseState;
            Vector2i janela = this.ClientRectangle.Size;
            var deltaX = (mouse.X - janela.X / 2) / (janela.X / 2);
            var deltaY = -((mouse.Y - janela.X / 2) / (janela.Y / 2));


            //Tarefas

            if (input.IsKeyPressed(Keys.Enter))
                ReiniciarFuncaoAtiva();
            if (input.IsKeyPressed(Keys.S))
                AtivarFuncaoSelecionarPoligono();
            if (input.IsKeyPressed(Keys.D))
                RemoverPoligono();
            if (input.IsKeyPressed(Keys.E))
                objetoSelecionado.RemoverVerticeMaisProximo(deltaX, deltaY);
            if (input.IsKeyPressed(Keys.P))
                objetoSelecionado.PrimitivaTipo = objetoSelecionado.PrimitivaTipo == PrimitiveType.LineLoop ? PrimitiveType.LineStrip : PrimitiveType.LineLoop;
            if (input.IsKeyPressed(Keys.KeyPad1))
                funcaoAtiva = Funcao.ADICIONAR_FILHO;

            // Atividade 6
            if (input.IsKeyPressed(Keys.R))
                objetoSelecionado.shaderCor = _shaderVermelha;
            if (input.IsKeyPressed(Keys.G))
                objetoSelecionado.shaderCor = _shaderVerde;
            if (input.IsKeyPressed(Keys.B))
                objetoSelecionado.shaderCor = _shaderAzul;

            //Outros
            if (input.IsKeyDown(Keys.Escape))
                Close();
            if (input.IsKeyPressed(Keys.G))
                mundo.GrafocenaImprimir("");
            if (input.IsKeyPressed(Keys.P))
                System.Console.WriteLine(objetoSelecionado.ToString());
            if (input.IsKeyPressed(Keys.M))
                objetoSelecionado.MatrizImprimir();
            //TODO: não está atualizando a BBox com as transformações geométricas
            if (input.IsKeyPressed(Keys.I))
                objetoSelecionado.MatrizAtribuirIdentidade();
            if (input.IsKeyPressed(Keys.Left))
                objetoSelecionado.MatrizTranslacaoXYZ(-0.05, 0, 0);
            if (input.IsKeyPressed(Keys.Right))
                objetoSelecionado.MatrizTranslacaoXYZ(0.05, 0, 0);
            if (input.IsKeyPressed(Keys.Up))
                objetoSelecionado.MatrizTranslacaoXYZ(0, 0.05, 0);
            if (input.IsKeyPressed(Keys.Down))
                objetoSelecionado.MatrizTranslacaoXYZ(0, -0.05, 0);
            if (input.IsKeyPressed(Keys.PageUp))
                objetoSelecionado.MatrizEscalaXYZ(2, 2, 2);
            if (input.IsKeyPressed(Keys.PageDown))
                objetoSelecionado.MatrizEscalaXYZ(0.5, 0.5, 0.5);
            if (input.IsKeyPressed(Keys.Home))
                objetoSelecionado.MatrizEscalaXYZBBox(0.5, 0.5, 0.5);
            if (input.IsKeyPressed(Keys.End))
                objetoSelecionado.MatrizEscalaXYZBBox(2, 2, 2);
            if (input.IsKeyPressed(Keys.D1))
                objetoSelecionado.MatrizRotacao(10);
            if (input.IsKeyPressed(Keys.D2))
                objetoSelecionado.MatrizRotacao(-10);
            if (input.IsKeyPressed(Keys.D3))
                objetoSelecionado.MatrizRotacaoZBBox(10);
            if (input.IsKeyPressed(Keys.D4))
                objetoSelecionado.MatrizRotacaoZBBox(-10);


            #endregion

            #region  Mouse

            if (mouse.IsButtonPressed(MouseButton.Left))
            {
                switch (funcaoAtiva)
                {
                    case Funcao.DEFAULT:
                        funcaoAtiva = Funcao.CRIAR_POLIGONO;
                        objetoSelecionado = new Poligono(mundo, ref rotuloAtual, new List<Ponto4D> { new Ponto4D(deltaX, deltaY), new Ponto4D(deltaX, deltaY) });
                        objetoSelecionado.PrimitivaTipo = PrimitiveType.LineLoop;
                        indiceVerticeSelecionado++;
                        break;
                    case Funcao.ADICIONAR_FILHO:
                        funcaoAtiva = Funcao.CRIAR_POLIGONO;
                        objetoSelecionado = new Poligono(objetoSelecionado, ref rotuloAtual, new List<Ponto4D> { new Ponto4D(deltaX, deltaY), new Ponto4D(deltaX, deltaY) });
                        objetoSelecionado.PrimitivaTipo = PrimitiveType.LineLoop;
                        indiceVerticeSelecionado++;
                        break;
                    case Funcao.MEXER_VERTICE:
                        funcaoAtiva = Funcao.DEFAULT;
                        indiceVerticeSelecionado = -1;
                        break;
                    case Funcao.CRIAR_POLIGONO:
                        objetoSelecionado.PrimitivaTipo = PrimitiveType.LineLoop;
                        objetoSelecionado.PontosAdicionar(new Ponto4D(deltaX, deltaY));
                        indiceVerticeSelecionado++;
                        break;
                    case Funcao.SELECIONAR_POLIGONO:
                        var objeto = mundo.SelecionarPoligono(new Ponto4D(deltaX, deltaY));
                        objetoSelecionado = objeto == null ? objetoSelecionado : objeto;
                        desenharBbox = objetoSelecionado != null;
                        break;
                }
            }

            if (mouse.IsButtonPressed(MouseButton.Right))
            {
                if (objetoSelecionado != null)
                {
                    funcaoAtiva = Funcao.MEXER_VERTICE;
                    indiceVerticeSelecionado = objetoSelecionado.BuscarOVerticeMaisProximo(deltaX, deltaY);
                }
            }

            switch (funcaoAtiva)
            {
                case Funcao.DEFAULT:
                    break;
                case Funcao.CRIAR_POLIGONO:
                    AtualizarObjetoSelecionadoParaOMouse(deltaX, deltaY);
                    break;
                case Funcao.MEXER_VERTICE:
                    if (indiceVerticeSelecionado > -1)
                    {
                        AtualizarObjetoSelecionadoParaOMouse(deltaX, deltaY, indiceVerticeSelecionado);
                    }
                    break;
                default:
                    break;
            }

            #endregion

        }

        private void RemoverPoligono()
        {
            mundo.RemoverObjeto(objetoSelecionado);
        }

        private void AtivarFuncaoSelecionarPoligono()
        {
            funcaoAtiva = Funcao.SELECIONAR_POLIGONO;
        }

        private void AtualizarObjetoSelecionadoParaOMouse(float deltaX, float deltaY, int indicePonto = 0)
        {
            objetoSelecionado.PontosAlterar(new Ponto4D(deltaX, deltaY, 0), indicePonto);
            objetoSelecionado.ObjetoAtualizar();
        }

        private void ReiniciarFuncaoAtiva()
        {
            funcaoAtiva = Funcao.DEFAULT;
            indiceVerticeSelecionado = -1;
            desenharBbox = false;
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
            GL.DeleteProgram(_shaderAmarela.Handle);

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


            if (objetoSelecionado != null)
            {
                var bboxSelecionada = objetoSelecionado.Bbox();
                var bboxArray = new float[]
                {
                    (float)bboxSelecionada.obterMenorX, (float)bboxSelecionada.obterMenorY, 0,        (float)bboxSelecionada.obterMaiorX, (float)bboxSelecionada.obterMenorY, 0,
                    (float)bboxSelecionada.obterMaiorX, (float)bboxSelecionada.obterMaiorY, 0,        (float)bboxSelecionada.obterMenorX, (float)bboxSelecionada.obterMaiorY, 0
                };
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, bboxArray.Length * sizeof(float), bboxArray, BufferUsageHint.StaticDraw);
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
            }


            if (desenharBbox)
            {
                GL.BindVertexArray(_vertexArrayObject);
                // EixoX
                _shaderAmarela.SetMatrix4("transform", transform);
                _shaderAmarela.Use();
                GL.DrawArrays(PrimitiveType.LineLoop, 0, 4);
            }
#elif CG_DirectX && !CG_OpenGL
      Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
      Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
        }
#endif

    }

    public enum Funcao
    {
        DEFAULT,
        MEXER_VERTICE,
        CRIAR_POLIGONO,
        SELECIONAR_POLIGONO,
        ADICIONAR_FILHO
    }
}
