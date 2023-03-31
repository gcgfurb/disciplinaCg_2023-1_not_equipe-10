#if CG_Gizmo
      if (bBoxDesenhar && (objetoSelecionado != null))
        objetoSelecionado.BBox.Desenhar();
#endif
      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.H)
        Utilitario.AjudaTeclado();
      else if (e.Key == Key.Escape)
        Exit();
      else if (e.Key == Key.E)    // N3-Exe4: ajuda a conferir se os poligonos e vértices estão certos
      {
        Console.WriteLine("--- Objetos / Pontos: ");
        for (var i = 0; i < objetosLista.Count; i++)
        {
          objetosLista[i].GrafocenaToString();
        }
      }
#if CG_Gizmo
      else if (e.Key == Key.O)
        bBoxDesenhar = !bBoxDesenhar;   // N3-Exe9: exibe a BBox ... sempre desenha bBox se tiver objetoSelecionado
#endif
      else if (e.Key == Key.Enter)
      {
        if (objetoSelecionado != null)
          objetoSelecionado.PontosBBoxAtualiza();
        if (objetoNovo != null)
        {
          objetoNovo.PontosRemoverUltimo();   // N3-Exe6: "truque" para deixar o rastro
          objetoSelecionado = objetoNovo;
          objetoNovo = null;
        }
        ptoMover = null;
      }
      else if (e.Key == Key.Space)
      {
        if (objetoNovo == null)
        {
          objetoId = Utilitario.charProximo(objetoId);
          if (objetoSelecionado == null)
          {
            objetoNovo = new Poligono(objetoId, null);
            objetosLista.Add(objetoNovo);
          }
          else
          {
            objetoNovo = new Poligono(objetoId, objetoSelecionado);
            objetoSelecionado.FilhoAdicionar(objetoNovo);
          }
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));  // N3-Exe6: "troque" para deixar o rastro
        }
        else
          objetoNovo.PontosAdicionar(new Ponto4D(mouseX, mouseY));
      }
      else if (e.Key == Key.A)
        GrafocenaBusca();

      else if (objetoSelecionado != null)
      {
        if (e.Key == Key.M)
          Console.WriteLine(objetoSelecionado.Matriz);
        else if (e.Key == Key.P)
          Console.WriteLine(objetoSelecionado);
        else if (e.Key == Key.I)
          objetoSelecionado.AtribuirIdentidade();
        //TODO: não está atualizando a BBox com as transformações geométricas
        else if (e.Key == Key.Left)
          objetoSelecionado.TranslacaoXYZ(-10, 0, 0);
        else if (e.Key == Key.Right)
          objetoSelecionado.TranslacaoXYZ(10, 0, 0);
        else if (e.Key == Key.Up)
          objetoSelecionado.TranslacaoXYZ(0, 10, 0);
        else if (e.Key == Key.Down)
          objetoSelecionado.TranslacaoXYZ(0, -10, 0);
        else if (e.Key == Key.PageUp)
          objetoSelecionado.EscalaXYZ(2, 2, 2);
        else if (e.Key == Key.PageDown)
          objetoSelecionado.EscalaXYZ(0.5, 0.5, 0.5);
        else if (e.Key == Key.Home)
          objetoSelecionado.EscalaXYZBBox(0.5, 0.5, 0.5);
        else if (e.Key == Key.End)
          objetoSelecionado.EscalaXYZBBox(2, 2, 2);
        else if (e.Key == Key.Number1)
          objetoSelecionado.Rotacao(10);
        else if (e.Key == Key.Number2)
          objetoSelecionado.Rotacao(-10);
        else if (e.Key == Key.Number3)
          objetoSelecionado.RotacaoZBBox(10);
        else if (e.Key == Key.Number4)
          objetoSelecionado.RotacaoZBBox(-10);
        else if (e.Key == Key.R) // N3-Exe08: interação cores
        {
          objetoSelecionado.ObjetoCor.CorR = 255;
          objetoSelecionado.ObjetoCor.CorG = 0;
          objetoSelecionado.ObjetoCor.CorB = 0;
        }
        else if (e.Key == Key.G) // N3-Exe08: interação cores
        {
          objetoSelecionado.ObjetoCor.CorR = 0;
          objetoSelecionado.ObjetoCor.CorG = 255;
          objetoSelecionado.ObjetoCor.CorB = 0;
        }
        else if (e.Key == Key.B) // N3-Exe08: interação cores
        {
          objetoSelecionado.ObjetoCor.CorR = 0;
          objetoSelecionado.ObjetoCor.CorG = 0;
          objetoSelecionado.ObjetoCor.CorB = 255;
        }
        else if (e.Key == Key.S)
          objetoSelecionado.PrimitivaTipoTroca();       // N3-Exe07: aberto ou fechado
        else if (e.Key == Key.D)
        {                                               // N3-Exe05: vértice: remover
          ptoMover = objetoSelecionado.PontosMaisPerto(new Ponto4D(mouseX, mouseY));
          if (ptoMover == null)
            objetoSelecionado.PontosBBoxAtualiza();
          else
            MundoGrafoCenaRemover();
        }
        else if (e.Key == Key.V)
        {                                               // N3-Exe05: vértice: mover
          ptoMover = objetoSelecionado.PontosMaisPerto(new Ponto4D(mouseX, mouseY), false);
        }
        else if (e.Key == Key.C)                        // N3-Exe04: remover poligono
          MundoGrafoCenaRemover();
        else if (e.Key == Key.X)
          objetoSelecionado.TrocaEixoRotacao('x');
        else if (e.Key == Key.Y)
          objetoSelecionado.TrocaEixoRotacao('y');
        else if (e.Key == Key.Z)
          objetoSelecionado.TrocaEixoRotacao('z');
        else
          Console.WriteLine(" __ Tecla não implementada.");
      }
      else
        Console.WriteLine(" __ Tecla não implementada.");
    }

    private void MundoGrafoCenaRemover()
    {
      objetoSelecionado.GrafocenaRemover();
      if (objetoSelecionado.PaiRef == null)
      {
        this.objetosLista.Remove(objetoSelecionado);
      }
      else
      {
        objetoSelecionado.PaiRef.FilhoRemover(objetoSelecionado);
      }
      objetoSelecionado = null;
    }

    //TODO: não está considerando o NDC
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      mouseX = e.Position.X; mouseY = 600 - e.Position.Y; // Inverti eixo Y
      if (objetoNovo != null)
      {
        objetoNovo.PontosUltimo().X = mouseX;
        objetoNovo.PontosUltimo().Y = mouseY;
      }
      else if (ptoMover != null)      // só move vertice senão estiver criando um novo poligono
      {
        ptoMover.X = mouseX;                              // N3-Exe5: movendo um vértice de um poligono específico
        ptoMover.Y = mouseY;
        objetoSelecionado.PontosBBoxAtualiza();
      }
    }

    private void GrafocenaBusca()
    {
      Objeto objeto = null;
      for (var i = 0; i < objetosLista.Count; i++)
      {
        objeto = objetosLista[i].GrafocenaBusca(new Ponto4D(mouseX, mouseY));
        if (objeto != null)
        {
          objetoSelecionado = objeto as ObjetoGeometria;
          break;
        }
      }
      if (objeto == null)
        objetoSelecionado = null;
      else
        objeto = null;
    }

#if CG_Gizmo
    private void Sru3D()
    {
#if CG_OpenGL
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      // GL.Color3(1.0f,0.0f,0.0f);
      GL.Color3(Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      // GL.Color3(0.0f,1.0f,0.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(255), Convert.ToByte(0));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      // GL.Color3(0.0f,0.0f,1.0f);
      GL.Color3(Convert.ToByte(0), Convert.ToByte(0), Convert.ToByte(255));
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
#endif
    }
#endif
  }
  class Program
  {
    static void Main(string[] args)
    {
      ToolkitOptions.Default.EnableHighResolution = false;
      Mundo window = Mundo.GetInstance(600, 600);
      window.Title = "CG_N3";
      window.Run(1.0 / 60.0);
    }
  }
}
