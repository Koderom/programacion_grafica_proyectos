using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ImGuiNET;
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
        ImGuiController _controller;
        Transform mTransform = new Transform();

        Shader shader;
        public Game(int width, int height, string title) :
            base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title })
        {

        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            escenario = ComponentUtils.loadData<Escenario>("./assets/escenario.json");
            //ComponentUtils.saveData("./assets/escenario.json", this.escenario);

            shader = new Shader("./shader.vert", "./shader.frag");

            Program = shader.Use();
            _controller = new ImGuiController(ClientSize.X, ClientSize.Y);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
            _controller.WindowResized(ClientSize.X, ClientSize.Y);
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            escenario.draw(Program);


            _controller.Update(this, (float)args.Time);
            loadPanel();
            _controller.Render();


            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape)) Close();

            mTransform = getSelectedTransform();
            bool isAnyShiftPressed = KeyboardState.IsKeyDown(Keys.RightShift) || KeyboardState.IsKeyDown(Keys.LeftShift);

            //Escala
            if (KeyboardState.IsKeyDown(Keys.E))
            {
                if (isAnyShiftPressed)
                    this.mTransform.Scale *= new Vector3(0.999f);
                else
                    this.mTransform.Scale *= new Vector3(1.001f);
            }

            //Traslacion
            float speed = 0.01f;
            if (KeyboardState.IsKeyDown(Keys.Up) && isAnyShiftPressed)
            {
                mTransform.Position += Vector3.UnitZ * speed;
            }
            else if (KeyboardState.IsKeyDown(Keys.Down) && isAnyShiftPressed)
            {
                mTransform.Position -= Vector3.UnitZ * speed;
            }
            else if (KeyboardState.IsKeyDown(Keys.Up))
            {
                mTransform.Position += Vector3.UnitY * speed;
            }
            else if (KeyboardState.IsKeyDown(Keys.Down))
            {
                mTransform.Position -= Vector3.UnitY * speed;
            }
            else if (KeyboardState.IsKeyDown(Keys.Right))
            {
                mTransform.Position += Vector3.UnitX * speed;
            }
            else if (KeyboardState.IsKeyDown(Keys.Left))
            {
                mTransform.Position -= Vector3.UnitX * speed;
            }

            //ROTACION
            float degreess = MathHelper.DegreesToRadians(0.1f);
            if (KeyboardState.IsKeyDown(Keys.X))
            {
                Matrix4 rotacion = Matrix4.CreateRotationX(degreess);
                this.mTransform.Rotation = rotacion * this.mTransform.Rotation;
            }
            if (KeyboardState.IsKeyDown(Keys.Y))
            {
                Matrix4 rotacion = Matrix4.CreateRotationY(degreess);
                this.mTransform.Rotation = rotacion * this.mTransform.Rotation;
            }
            if (KeyboardState.IsKeyDown(Keys.Z))
            {
                Matrix4 rotacion = Matrix4.CreateRotationZ(degreess);
                this.mTransform.Rotation = rotacion * this.mTransform.Rotation;
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


        private void loadPanel()
        {
            String root = "escenario";
            if (ImGui.Begin("Escenario"))
            {
                bool escOpen = ImGui.TreeNode(root);
                drawPanelNodoSeleccionable(root,root);

                if (escOpen) {
                    foreach (var (objNombre, objeto) in escenario.objetos)
                    {
                        bool objOpen = ImGui.TreeNode(objNombre);
                        drawPanelNodoSeleccionable(objNombre, $"{root}/{objNombre}");
                        if (objOpen)
                        {
                            foreach (var (elNombre, elemento) in objeto.elementos)
                            {
                                bool elOpen = ImGui.TreeNode(elNombre);
                                drawPanelNodoSeleccionable(elNombre, $"{root}/{objNombre}/{elNombre}");
                                if (elOpen)
                                {
                                    foreach (var (caraNombre, cara) in elemento.caras)
                                    {
                                        drawPanelNodoSeleccionable(caraNombre, $"{root}/{objNombre}/{elNombre}/{caraNombre}");
                                    }
                                    ImGui.TreePop(); // Elemento
                                }
                            }
                            ImGui.TreePop(); // Objeto
                        }
                    }
                    ImGui.TreePop();
                }
            }
            ImGui.End();
        }
        private string _selectedPath = "";

        private void drawPanelNodoSeleccionable(string nombre, string path)
        {
            bool isSelected = _selectedPath == path;
            if (ImGui.Selectable($"-> {nombre}", isSelected))
            {
                _selectedPath = path;
                Console.WriteLine($"Seleccionado: {path}");
            }
        }

        private Transform getSelectedTransform() {
            if (_selectedPath.Length == 0) return escenario.transform;
            String[] path = _selectedPath.Split("/");
            switch (path.Length - 1)
            {
                case 0: return escenario.transform;
                case 1: return escenario.getObject(path[1]).transform;
                case 2: return escenario.getObject(path[1]).getElemento(path[2]).transform;
                case 3: return escenario.getObject(path[1]).getElemento(path[2]).GetCara(path[3]).transform;
            }
            return escenario.transform;
        }
    }
}
