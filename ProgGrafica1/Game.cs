using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ProgGrafica1.Elements;

namespace ProgGrafica1
{
    public class Game : GameWindow
    {
        Escenario escenario;

        Shader shader;
        public Game(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) {
            
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            loadEscenario("./assets/escenario.json");
            //saveEscenario("./assets/escenario.json");

            shader = new Shader("./shader.vert", "./shader.frag");

            shader.Use();
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
            if (KeyboardState.IsKeyDown(Keys.Escape)) {
                Close();
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
