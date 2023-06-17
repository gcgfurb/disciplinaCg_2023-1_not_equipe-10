//https://github.com/mono/opentk/blob/main/Source/Examples/Shapes/Old/Cube.cs

#define CG_Debug
using CG_Biblioteca;
using OpenTK.Mathematics;
using System.Drawing;

namespace gcgcg
{
    internal class Cubo : Objeto
    {
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
               0, 1, 2, 3, // front face
               3, 7, 5, 2, // right face
               2, 5, 4, 1, // top face
               1, 0, 6, 4, // left face
               4, 5, 7, 6, // back face
               6, 7, 3, 0, // bottom face  
            };

            // normals = new Vector3[]
            // {
            //   new Vector3(-1.0f, -1.0f,  1.0f),
            //   new Vector3( 1.0f, -1.0f,  1.0f),
            //   new Vector3( 1.0f,  1.0f,  1.0f),
            //   new Vector3(-1.0f,  1.0f,  1.0f),
            //   new Vector3(-1.0f, -1.0f, -1.0f),
            //   new Vector3( 1.0f, -1.0f, -1.0f),
            //   new Vector3( 1.0f,  1.0f, -1.0f),
            //   new Vector3(-1.0f,  1.0f, -1.0f),
            // };

            // colors = new int[]
            // {
            //   ColorToRgba32(Color.DarkRed),
            //   ColorToRgba32(Color.DarkRed),
            //   ColorToRgba32(Color.Gold),
            //   ColorToRgba32(Color.Gold),
            //   ColorToRgba32(Color.DarkRed),
            //   ColorToRgba32(Color.DarkRed),
            //   ColorToRgba32(Color.Gold),
            //   ColorToRgba32(Color.Gold),
            // };

            // Sentido horário
            for (int i = 0; i < indices.Length; i+=4)
            {
                var verticeA = vertices[indices[i]];
                var verticeB = vertices[indices[i + 1]];
                var verticeC = vertices[indices[i + 2]];
                var verticeD = vertices[indices[i + 3]];
                base.PontosAdicionar(verticeA);
                base.PontosAdicionar(verticeB);
                base.PontosAdicionar(verticeC);
                base.PontosAdicionar(verticeD);
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
