using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ProgGrafica1
{
    public class Game : GameWindow
    {
        float[] vertices = {
            -0.3f, 0.5f, 0.1f,
            -0.1f, 0.5f, 0.1f,
            0.1f, 0.5f, 0.1f,
            0.3f, 0.5f, 0.1f,
            0.3f, -0.2f, 0.1f,
            0.3f, -0.4f, 0.1f,
            -0.3f, -0.4f, 0.1f,
            -0.3f, -0.2f, 0.1f,
            -0.1f, -0.2f, 0.1f,
            0.1f, -0.2f, 0.1f,

            -0.3f, 0.5f, -0.1f,
            -0.1f, 0.5f, -0.1f,
            0.1f, 0.5f, -0.1f,
            0.3f, 0.5f, -0.1f,
            0.3f, -0.2f, -0.1f,
            0.3f, -0.4f, -0.1f,
            -0.3f, -0.4f, -0.1f,
            -0.3f, -0.2f, -0.1f,
            -0.1f, -0.2f, -0.1f,
            0.1f, -0.2f, -0.1f,
        };

        uint[] indices = {
            0,1,8, 0,8,7, 2,3,9,
            3,4,9, 7,4,6, 4,5,6,

            10,11,18, 10,18,17, 12,13,19,
            13,14,19, 17,14,16, 14,15,16,

            5,15,6, 6,16,15,
            6,16,0, 0,10,16,
            3,5,15, 15,13,3,

            1,10,8, 8,18,10,
            18,8,9, 9,19,18,
            2,12,9, 9,19,12,

            0,1,10, 10,11,1,
            2,3,13, 13,2,12
        };

        int VertexbufferObject;
        int VertexArrayObject;
        int ElementBufferObject;

        float[] rotacion = { 0.0f, 0.0f, 0.0f };
        double acumUpdateFrame = 0;

        Shader shader;
        public Game(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) {
            
        }
        //Init code here
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            VertexbufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexbufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StreamDraw);


            shader = new Shader("./shader.vert", "./shader.frag");
            shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            //GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            acumUpdateFrame += args.Time;

            if (KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }
            if (acumUpdateFrame >= 0.005) {
               
                if (KeyboardState.IsKeyDown(Keys.Right))
                {
                    float rotarX = rotacion[0] + 1;
                    if (rotarX > 360) rotarX = 0.0f;
                    rotacion[0] = rotarX;
                    shader.rotar(rotarX, 2);
                }

                if (KeyboardState.IsKeyDown(Keys.Left))
                {
                    float rotarX = rotacion[0] - 1;
                    if (rotarX < 0) rotarX = 360.0f;
                    rotacion[0] = rotarX;
                    shader.rotar(rotarX, 2);
                }

                if (KeyboardState.IsKeyDown(Keys.Up))
                {
                    float rotarY = rotacion[1] + 1;
                    if (rotarY > 360) rotarY = 0.0f;
                    rotacion[1] = rotarY;
                    shader.rotar(rotarY, 1);
                }

                if (KeyboardState.IsKeyDown(Keys.Down))
                {
                    float rotarY = rotacion[1] - 1;
                    if (rotarY < 0) rotarY = 360.0f;
                    rotacion[1] = rotarY;
                    shader.rotar(rotarY, 1);
                }
                acumUpdateFrame = 0;
            }
            
        }
        

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(VertexbufferObject);
            GL.DeleteVertexArray(VertexArrayObject);

            shader.Dispose();
        }
    }
}
