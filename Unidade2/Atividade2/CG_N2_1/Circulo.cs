using gcgcg;
using CG_Biblioteca;

internal class Circulo : Objeto {
    public Circulo(char rotulo, Objeto paiRef) : base(rotulo, paiRef)
    {
      for (int i = 0; i < 360; i+=5)
      {
        Ponto4D ponto = Matematica.GerarPtosCirculo(i, 0.25);
        this.PontosAdicionar(ponto);
      }
    }
}