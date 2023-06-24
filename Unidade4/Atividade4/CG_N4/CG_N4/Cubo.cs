//https://github.com/mono/opentk/blob/main/Source/Examples/Shapes/Old/Cube.cs

#define CG_Debug
using CG_Biblioteca;
using OpenTK.Mathematics;
using System.Drawing;

namespace gcgcg
{
    internal class Cubo : Objeto
    {
        int A = 0;
        int B = 1;
        int C = 2;
        int D = 3;
        int E = 4;
        int F = 5;
        int G = 6;
        int H = 7;


        Ponto4D[] vertices;
        int[] indices;
        Vector3[] normals;
        int[] colors;

        public Cubo(Objeto paiRef, ref char _rotulo) :
        this(paiRef, ref _rotulo, new Ponto4D(-0.5, -0.5), new Ponto4D(0.5, 0.5))
        { }

        public Cubo(Objeto paiRef, ref char _rotulo, Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base(paiRef, ref _rotulo)
        {
            vertices = new Ponto4D[]
            {
                 new Ponto4D(ptoInfEsq.X, ptoInfEsq.Y, ptoSupDir.Z),  // A  0
                 new Ponto4D(ptoInfEsq.X, ptoSupDir.Y, ptoSupDir.Z),  // B  1
                 new Ponto4D(ptoSupDir.X, ptoSupDir.Y, ptoSupDir.Z),  // C  2
                 new Ponto4D(ptoSupDir.X, ptoInfEsq.Y, ptoSupDir.Z),  // D  3
                 new Ponto4D(ptoInfEsq.X, ptoSupDir.Y, ptoInfEsq.Z),  // E  4
                 new Ponto4D(ptoSupDir.X, ptoSupDir.Y, ptoInfEsq.Z),  // F  5
                 new Ponto4D(ptoInfEsq.X, ptoInfEsq.Y, ptoInfEsq.Z),  // G  6
                 new Ponto4D(ptoSupDir.X, ptoInfEsq.Y, ptoInfEsq.Z),  // H  7
            };

            indices = new int[]
            {
               1, // Front-top-left
               2, // Front-top-right
               0, // Front-bottom-left
               3, // Front-bottom-right
               7, // Back-bottom-right
               2, // Front-top-right
               5, // Back-top-right
               1, // Front-top-left
               4, // Back-top-left
               0, // Front-bottom-left
               6, // Back-bottom-left
               7, // Back-bottom-right
               4, // Back-top-left
               5  // Back-top-right
            };
            
            for (int i = 0; i < indices.Length; i ++)
            {
                var verticeA = vertices[indices[i]];
                base.PontosAdicionar(verticeA);
            }
            Atualizar();
        }

        public static int ColorToRgba32(Color c)
        {
            return (int)((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
        }

        private void Atualizar()
        {

            base.ObjetoAtualizar();
        }

#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Cubo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
            retorno += base.ImprimeToString();
            return (retorno);
        }
#endif

    }
}
