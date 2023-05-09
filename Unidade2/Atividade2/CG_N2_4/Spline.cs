#define CG_Debug

using CG_Biblioteca;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
    internal class Spline : Objeto
    {
        public double quantidadePontos { get; set; }
        private Objeto _paiRef { get; set; }

        private Ponto4D pontoA;
        private Ponto4D pontoB;
        private Ponto4D pontoC;
        private Ponto4D pontoD;
        public List<Ponto4D> resultados = new List<Ponto4D>();

        public Spline(Objeto paiRef, Ponto4D pontoA, Ponto4D pontoB, Ponto4D pontoC, Ponto4D pontoD, int quantidadePontos) : base(paiRef)
        {
            this.quantidadePontos = quantidadePontos;
            this._paiRef = paiRef;
            this.pontoA = pontoA;
            this.pontoB = pontoB;
            this.pontoC = pontoC;
            this.pontoD = pontoD;

            DesenharSpline();
        }

        protected void DesenharSpline()
        {

            for (double i = 0; i < quantidadePontos; i++)
            {
                double t = i / quantidadePontos;
                Ponto4D pontoAB = Calcular(pontoA, pontoB, t);
                Ponto4D pontoBC = Calcular(pontoB, pontoC, t);
                Ponto4D pontoCD = Calcular(pontoC, pontoD, t);
                Ponto4D pontoX = Calcular(pontoAB, pontoBC, t);
                Ponto4D pontoY = Calcular(pontoBC, pontoCD, t);
                Ponto4D resultado = Calcular(pontoX, pontoY, t);
                resultados.Add(resultado);
            }
        }

        private Ponto4D Calcular(Ponto4D pontoX, Ponto4D pontoY, double t)
        {
            double X = pontoX.X + (pontoY.X - pontoX.X) * t;
            double Y = pontoX.Y + (pontoY.Y - pontoX.Y) * t;

            return new Ponto4D(X, Y);
        }


    }
}
