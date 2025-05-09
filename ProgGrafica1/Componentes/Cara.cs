using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using ProgGrafica1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Cara
    {
        public Transform transform { get; set; } = new Transform();
        public List<uint> indices { get; set; }
        public List<Punto> vertices { get; set; }

        private int VBO = 0;
        private int EBO = 0;
        private int VAO = 0;
        
        private bool isBinding = false;

        public Cara(List<uint> indices = null, List<Punto> vertices = null)
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            EBO = GL.GenBuffer();
            

            this.indices = indices??new List<uint>();
            this.vertices = vertices ?? new List<Punto>();
            this.transform = new Transform();
        }

        public void add(uint pointIndexA, uint pointIndexB, uint pointIndexC)
        {
            indices.Add(pointIndexA);
            indices.Add(pointIndexB);
            indices.Add(pointIndexC);
        }

        public void draw(int program, Transform transform, Punto origen)
        {
            Transform rtransform = Transform.combinarTransformacion(transform, this.transform);
            GL.BindVertexArray(VAO);
            
            if (!isBinding)
            {
                List<Punto> fixedVertices = ComponentUtils.fixToAbsoluteOrigin(origen, vertices);
                float[] arrayVertices = ComponentUtils.pointListToArray(fixedVertices);

                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BufferData( BufferTarget.ArrayBuffer, arrayVertices.Length * sizeof(float), arrayVertices, BufferUsageHint.StaticDraw);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(), BufferUsageHint.StreamDraw);

                isBinding = true;
            }
            else
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            }
            drawTransform(program, rtransform, origen);
            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
        }
        private void drawTransform(int program, Transform transform, Punto punto) {
            Matrix4 mTranform = transform.GetModelMatrix(punto);
            
            int location = GL.GetUniformLocation(program, "transform");
            if (location != -1)
            {
                GL.UniformMatrix4(location, false, ref mTranform);
            }
        }
        public void dispose() {
            GL.DeleteVertexArray(VAO);
        }
    }
}
