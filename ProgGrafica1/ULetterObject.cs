using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1
{
    class ULetterObject
    {
        int VBO, VAO, EBO;
        float pos_x = 0, pos_y = 0, pos_z = 0;

        float[] vertices = defaultVertices;
        uint[] indices = defaulIndices;

        public ULetterObject(float pos_x = 0, float pos_y = 0, float pos_z = 0) {
            this.pos_x = pos_x;
            this.pos_y = pos_y;
            this.pos_z = pos_z;

            generarObjeto();

            VBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            VAO = GL.GenVertexArray();
            GL.BindVertexArray(VAO);

            EBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StreamDraw);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public void updateValues(float pos_x, float pos_y, float pos_z) {
            this.pos_x = pos_x;
            this.pos_y = pos_y;
            this.pos_z = pos_z;

            generarObjeto();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, vertices.Length * sizeof(float), vertices);
        }

        public void draw() {
            GL.BindVertexArray(VAO);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        private void generarObjeto()
        {
            generarVertices();
            generarIndices();
        }

        public float[] generarVertices() {
            float[] vertices = defaultVertices.ToArray();

            for (int i = 0; i < vertices.Length; i++) {

                switch ( (i + 1) % 3 ) {
                    case 1:
                        vertices[i] = vertices[i] + this.pos_x;
                        break;
                    case 2:
                        vertices[i] = vertices[i] + this.pos_y;
                        break;
                    case 0:
                        vertices[i] = vertices[i] + this.pos_z;
                        break;
                }
            }

            this.vertices = vertices;
            return vertices;
        }

        public uint[] generarIndices() {
            uint[] indices = defaulIndices;
            this.indices = indices;
            return this.indices;
        }

        private static float[] defaultVertices = {
            -0.3f, 0.5f, 0.1f,      -0.1f, 0.5f, 0.1f,      0.1f, 0.5f, 0.1f,
            0.3f, 0.5f, 0.1f,       0.3f, -0.3f, 0.1f,      0.3f, -0.5f, 0.1f,
            -0.3f, -0.5f, 0.1f,     -0.3f, -0.3f, 0.1f,     -0.1f, -0.3f, 0.1f,
            0.1f, -0.3f, 0.1f,

            -0.3f, 0.5f, -0.1f,     -0.1f, 0.5f, -0.1f,     0.1f, 0.5f, -0.1f,
            0.3f, 0.5f, -0.1f,      0.3f, -0.3f, -0.1f,     0.3f, -0.5f, -0.1f,
            -0.3f, -0.5f, -0.1f,    -0.3f, -0.3f, -0.1f,    -0.1f, -0.3f, -0.1f,
            0.1f, -0.3f, -0.1f,
        };

        private static uint[] defaulIndices = {
            0,1,8, 0,7,8,       4,7,6, 6,5,4,       2,3,9, 3,4,9,

            10,11,18, 10,17,18, 14,17,16, 16,15,14, 12,13,19, 13,14,19,

            5,15,6, 6,16,15,    6,16,0, 0,10,16,    3,5,15, 15,13,3,

            1,11,8, 8,18,11,    18,8,9, 9,19,18,    2,12,9, 9,19,12,

            0,1,10, 10,11,1,    2,3,13, 13,2,12
        };
    }
}
