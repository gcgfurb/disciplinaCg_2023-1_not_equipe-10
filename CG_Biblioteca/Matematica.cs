using System;

namespace CG_Biblioteca
{
  /// <summary>
  /// Classe com funções matemáticas.
  /// </summary>
  public abstract class Matematica
  {
    /// <summary>
    /// Função para calcular um ponto sobre o perímetro de um círculo informando um ângulo e raio.
    /// </summary>
    /// <param name="angulo"></param>
    /// <param name="raio"></param>
    /// <returns></returns>
    public static Ponto4D GerarPtosCirculo(double angulo, double raio)
    {
      Ponto4D pto = new Ponto4D();
      pto.X = (raio * Math.Cos(Math.PI * angulo / 180.0));
      pto.Y = (raio * Math.Sin(Math.PI * angulo / 180.0));
      pto.Z = 0;
      return (pto);
    }

    public static double GerarPtosCirculoSimetrico(double raio)
    {
      return (raio * Math.Cos(Math.PI * 45 / 180.0));
    }

    public static bool Dentro(BBox bBox, Ponto4D pto)
    {
      if ((pto.X >= bBox.obterMenorX && pto.X <= bBox.obterMaiorX) &&
          (pto.Y >= bBox.obterMenorY && pto.Y <= bBox.obterMaiorY) &&
          (pto.Z >= bBox.obterMenorZ && pto.Z <= bBox.obterMaiorZ))
      {
        return true;
      }
      return false;
    }

    public static double distanciaQuadrado(Ponto4D ptoA, Ponto4D ptoB)
    {
      return (
        Math.Pow((ptoB.X - ptoA.X), 2) +
          Math.Pow((ptoB.Y - ptoA.Y), 2)
      );
    }

    public static double distancia(Ponto4D ptoA, Ponto4D ptoB)
    {
      return (
        Math.Sqrt(distanciaQuadrado(ptoA,ptoB))
      );
    }

  }
}