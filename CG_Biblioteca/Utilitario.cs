using System;

namespace gcgcg
{
  public abstract class Utilitario
  {
    public static char charProximo(char atual) {
      return Convert.ToChar(atual + 1);
    }
    public static void AjudaTeclado()
    {
      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H     ] mostra está ajuda. ");
      Console.WriteLine(" [Escape  ] sair. ");
    }

  }
}