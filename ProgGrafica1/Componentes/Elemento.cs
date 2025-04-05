using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Elemento
    {
        private int VBO = 0;
        private bool isBinding = false;

        public List<Punto> vertices { get; set; }
        public List<Cara> caras { get; set; }
        public Punto position { get; set; }


        public Elemento(Punto position = null, List<Punto> vertices = null, List<Cara> caras = null)
        {
            this.position = position?? new Punto();
            this.vertices = vertices ?? new List<Punto>();
            this.caras = caras ?? new List<Cara>();

            VBO = GL.GenBuffer();
        }

        public void add(Cara cara)
        {
            caras.Add(cara);
        }

        public void draw(Punto relativeOrigin)
        {
            if (!isBinding)
            {
                float[] arrayVertices = getArrayVertices(relativeOrigin);

                VBO = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    arrayVertices.Length * sizeof(float),
                    arrayVertices,
                    BufferUsageHint.StaticDraw
                );

                isBinding = true;

            }
            else {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            }

            foreach (Cara cara in caras) cara.draw();
        }
        private float[] getArrayVertices(Punto relativeOrigin) { 
            List<float> vertices = new List<float>();
            foreach (Punto item in this.vertices) { 
                vertices.Add(item.x);
                vertices.Add(item.y);
                vertices.Add(item.z);
            }

            return fixedToOrigin(Punto.calculateResultPoint(relativeOrigin, position), vertices.ToArray());
        }

        private float[] fixedToOrigin(Punto relativeOrigin, float[] vertices)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                switch ((i + 1) % 3)
                {
                    case 1:
                        vertices[i] = vertices[i] + relativeOrigin.x;
                        break;
                    case 2:
                        vertices[i] = vertices[i] + relativeOrigin.y;
                        break;
                    case 0:
                        vertices[i] = vertices[i] + relativeOrigin.z;
                        break;
                }
            }
            return vertices;
        }

        public void dispose()
        {
            foreach (Cara cara in caras) cara.dispose();
        }
    }
}
