namespace GrafoCena
{
  internal class Objeto
  {
    private List<Objeto> objetoLista = new List<Objeto>();
    private readonly char rotulo;
    // public char Rotulo { get => rotulo; set => rotulo = value; }

    public Objeto(ref char _rotulo)
    {
      rotulo = _rotulo = charProximo(_rotulo);
      System.Console.WriteLine("Objeto: " + rotulo);
    }

    public void ObjetoFilhoAdicionar(ref char _rotulo)
    {
      Objeto objFilho = new Objeto(ref _rotulo);
      objetoLista.Add(objFilho);
    }

    public Objeto? GrafocenaBusca(char _rotulo) {
      if (rotulo == _rotulo)
      {
        return this;
      }
      foreach (var objeto in objetoLista)
      {
        var obj = objeto.GrafocenaBusca(_rotulo);
        if (obj != null)
        {
          return obj;
        }
      }
      return null;
    }

    public void GrafocenaImprimir(String idt)
    {
      System.Console.WriteLine(idt + rotulo);
      foreach (var objeto in objetoLista)
      {
        objeto.GrafocenaImprimir(idt + "  ");
      }
    }

    private char charProximo(char atual) {
      return Convert.ToChar(atual + 1);
    }

  }
}