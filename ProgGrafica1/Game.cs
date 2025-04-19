using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ProgGrafica1.Elements;
using ProgGrafica1.utils;

namespace ProgGrafica1
{
    public class Game : GameWindow
    {
        int Program = -1;
        Escenario escenario;
        Transform mTransform = new Transform();

        Shader shader;
        public Game(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) {
            
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            escenario = ComponentUtils.loadData<Escenario>("./assets/escenario.json");
            //ComponentUtils.saveData("./assets/escenario.json", this.escenario);

            shader = new Shader("./shader.vert", "./shader.frag");

            Program = shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            escenario.draw();

            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            Matrix4 tranform = Matrix4.Identity;
            

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyDown(Keys.M)) {
                this.mTransform.Scale *= new Vector3(1.0001f);
            }
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                this.mTransform.Scale *= new Vector3(0.9999f);
            }
            if (KeyboardState.IsKeyDown(Keys.X))
            {
                Matrix4 rotacion = Matrix4.CreateRotationX(
                    MathHelper.DegreesToRadians(0.1f)
                );
                this.mTransform.Rotation = rotacion * this.mTransform.Rotation;
            }
            if (KeyboardState.IsKeyDown(Keys.Y))
            {
                Matrix4 rotacion = Matrix4.CreateRotationY(
                    MathHelper.DegreesToRadians(0.1f)
                );
                this.mTransform.Rotation = rotacion * this.mTransform.Rotation;
            }
            if (KeyboardState.IsKeyDown(Keys.Z))
            {
                Matrix4 rotacion = Matrix4.CreateRotationZ(
                    MathHelper.DegreesToRadians(0.1f)
                );
                this.mTransform.Rotation = rotacion * this.mTransform.Rotation;
            }
            
            if (KeyboardState.IsKeyDown(Keys.Up))
                mTransform.Position.Y += 0.001f;
            if (KeyboardState.IsKeyDown(Keys.Down))
                mTransform.Position.Y -= 0.001f;
            if (KeyboardState.IsKeyDown(Keys.Left))
                mTransform.Position.X -= 0.001f;
            if (KeyboardState.IsKeyDown(Keys.Right))
                mTransform.Position.X += 0.001f;

            tranform = mTransform.GetModelMatrix();
            int location = GL.GetUniformLocation(Program, "transform");
            if (location != -1)
            {
                GL.UniformMatrix4(location, false, ref tranform);
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

            //TODO: hacer esto para cada objeto
            //GL.DeleteBuffer(VertexbufferObject);
            //GL.DeleteVertexArray(VertexArrayObject);

            shader.Dispose();
        }

        private void saveEscenario(String path) {
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDir, path);

            string jsonEscenario = JsonSerializer.Serialize(this.escenario);
            File.WriteAllText (filePath, jsonEscenario);
        }

        private void loadEscenario(String path){
            try
            {
                string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string filePath = Path.Combine(projectDir, path);

                string jsonString = File.ReadAllText(filePath);
                this.escenario = JsonSerializer.Deserialize<Escenario>(jsonString);
            }
            catch (Exception e)
            {
                escenario = new Escenario();
            }
        }
    }
}
