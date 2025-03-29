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
        List<ULetterObject> uLetterObjectsList = new List<ULetterObject>();

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

            uLetterObjectsList = new List<ULetterObject>{
                new ULetterObject(0.4f, 0.0f, 0.0f),
                new ULetterObject(-0.4f, 0.0f, 0.0f)
            };

            shader = new Shader("./shader.vert", "./shader.frag");
            shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach(ULetterObject item in uLetterObjectsList ) {
                item.draw();
            }

            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            acumUpdateFrame += args.Time;

            if (KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
            }
            //if (acumUpdateFrame >= 0.005) {
               
            //    if (KeyboardState.IsKeyDown(Keys.Right))
            //    {
            //        float rotarX = rotacion[0] + 1;
            //        if (rotarX > 360) rotarX = 0.0f;
            //        rotacion[0] = rotarX;
            //        shader.rotar(rotarX, 2);
            //    }

            //    if (KeyboardState.IsKeyDown(Keys.Left))
            //    {
            //        float rotarX = rotacion[0] - 1;
            //        if (rotarX < 0) rotarX = 360.0f;
            //        rotacion[0] = rotarX;
            //        shader.rotar(rotarX, 2);
            //    }

            //    if (KeyboardState.IsKeyDown(Keys.Up))
            //    {
            //        float rotarY = rotacion[1] + 1;
            //        if (rotarY > 360) rotarY = 0.0f;
            //        rotacion[1] = rotarY;
            //        shader.rotar(rotarY, 1);
            //    }

            //    if (KeyboardState.IsKeyDown(Keys.Down))
            //    {
            //        float rotarY = rotacion[1] - 1;
            //        if (rotarY < 0) rotarY = 360.0f;
            //        rotacion[1] = rotarY;
            //        shader.rotar(rotarY, 1);
            //    }
            //    acumUpdateFrame = 0;
            //}
            
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

            //TODO: hacer esto para cada objeto
            //GL.DeleteBuffer(VertexbufferObject);
            //GL.DeleteVertexArray(VertexArrayObject);

            shader.Dispose();
        }
    }
}
