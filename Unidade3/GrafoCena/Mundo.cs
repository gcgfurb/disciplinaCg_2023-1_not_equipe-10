namespace GrafoCena
{
  internal class Mundo
  {
    private List<Objeto> objetosLista = new List<Objeto>();
    private readonly char rotulo = '@';
    private char rotuloAtual;

    public Mundo()
    {
      rotuloAtual = rotulo;

      Objeto objeto = new Objeto(ref rotuloAtual);
      objetosLista.Add(objeto);

      objeto = new Objeto(ref rotuloAtual);
      objetosLista.Add(objeto);
      objeto.ObjetoFilhoAdicionar(ref rotuloAtual);
      objeto.ObjetoFilhoAdicionar(ref rotuloAtual);

      objeto = new Objeto(ref rotuloAtual);
      objetosLista.Add(objeto);

      System.Console.WriteLine("------------------");
      System.Console.WriteLine("Mundo");
      foreach (var obj in objetosLista)
      {
        obj.GrafocenaImprimir("  ");
      }
      System.Console.WriteLine("------------------");

      foreach (var obj in objetosLista)
      {
        System.Console.WriteLine(" _Informe ID Grafo Cena: ");
        var objBuscar = obj.GrafocenaBusca('C');
        if (objBuscar != null)
        {
          objBuscar.ObjetoFilhoAdicionar(ref rotuloAtual);
          System.Console.WriteLine("Achou!!");
          break;
        }
      }
      System.Console.WriteLine("..");

      System.Console.WriteLine("------------------");
      System.Console.WriteLine("Mundo");
      foreach (var obj in objetosLista)
      {
        obj.GrafocenaImprimir("  ");
      }
      System.Console.WriteLine("------------------");

    }

  }

}