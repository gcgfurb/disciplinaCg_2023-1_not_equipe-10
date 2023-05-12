using gcgcg;
using CG_Biblioteca;

internal class Circulo : Objeto
{
    public Circulo(Objeto paiRef, double x, double y, double raio) : base(paiRef)
    {
        for (int i = 0; i < 360; i += 5)
        {
            Ponto4D ponto = Matematica.GerarPtosCirculo(i, raio);
            ponto.X += x;
            ponto.Y += y;
            this.PontosAdicionar(ponto);
        }
    }

    public void PontosAlterar(double x, double y)
    {
        for (int i = 0; i < pontosLista.Count; i++)
        {
            pontosLista[i].X += x;
            pontosLista[i].Y += y;
        }
    }
}