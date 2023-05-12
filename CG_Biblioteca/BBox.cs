#define CG_Debug

using System.Collections.Generic;

namespace CG_Biblioteca
{
  public class BBox
  {
    private double menorX, menorY, menorZ, maiorX, maiorY, maiorZ;
    private Ponto4D centro = new Ponto4D();

    public BBox()
    {
    }

    public void Atualizar(List<Ponto4D> pontosLista)
    {
      this.menorX = pontosLista[0].X; this.menorY = pontosLista[0].Y; this.menorZ = pontosLista[0].Z;
      this.maiorX = pontosLista[0].X; this.maiorY = pontosLista[0].Y; this.maiorZ = pontosLista[0].Z;

      for (var i = 1; i < pontosLista.Count; i++)
      {
        Atualizar(pontosLista[i].X,pontosLista[i].Y,pontosLista[i].Z);
      }
      
      ProcessarCentro();
    }

    private void Atualizar(double x, double y, double z)
    {
      if (x < menorX)
        menorX = x;
      else
      {
        if (x > maiorX) maiorX = x;
      }
      if (y < menorY)
        menorY = y;
      else
      {
        if (y > maiorY) maiorY = y;
      }
      if (z < menorZ)
        menorZ = z;
      else
      {
        if (z > maiorZ) maiorZ = z;
      }
    }

    public void ProcessarCentro()
    {
      centro.X = (maiorX + menorX) / 2;
      centro.Y = (maiorY + menorY) / 2;
      centro.Z = (maiorZ + menorZ) / 2;
    }

    /// Obter menor valor X da BBox.
    public double obterMenorX => menorX;

    /// Obter menor valor Y da BBox.
    public double obterMenorY => menorY;

    /// Obter menor valor Z da BBox.
    public double obterMenorZ => menorZ;

    /// Obter maior valor X da BBox.
    public double obterMaiorX => maiorX;

    /// Obter maior valor Y da BBox.
    public double obterMaiorY => maiorY;

    /// Obter maior valor Z da BBox.
    public double obterMaiorZ => maiorZ;

    /// Obter ponto do centro da BBox.
    public Ponto4D obterCentro => centro;

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "_____ BBox: \n";
      retorno += "menorX: " + menorX + " - maiorX: " + maiorX + "\n";
      retorno += "menorY: " + menorY + " - maiorY: " + maiorY + "\n";
      retorno += "menorZ: " + menorZ + " - maiorZ: " + maiorZ + "\n";
      retorno += "  centroX: " + centro.X + " - centroY: " + centro.Y + " - centroZ: " + centro.Z+ "\n";
      retorno += "__________________________________ \n";
      return (retorno);
    }
#endif

  }
}