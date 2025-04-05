using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Cara
    {
        private int EBO = 0;
        private bool isBinding = false;

        public List<uint> indices { get; set; }

        public Cara(List<uint> indices = null)
        {
            EBO = GL.GenBuffer();

            this.indices = indices??new List<uint>();
        }

        public void add(uint pointIndexA, uint pointIndexB, uint pointIndexC)
        {
            indices.Add(pointIndexA);
            indices.Add(pointIndexB);
            indices.Add(pointIndexC);
        }

        public void draw()
        {
            if (!isBinding) { 
                EBO = GL.GenBuffer();

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StreamDraw);

                isBinding = true;
            }
            else{
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            }

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void dispose() { 
            //TODO: implement dispose
        }
    }
}
