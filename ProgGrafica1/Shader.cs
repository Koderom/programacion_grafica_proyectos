﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    public class Shader
    {
        public int Handle;
        int VertexShader;
        int FragmentShader;

        public Shader(string vertexPath, string fragmentPath)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string VertexShaderSource = File.ReadAllText( Path.Combine(basePath, vertexPath));
            string FragmentShaderSource = File.ReadAllText(Path.Combine(basePath, fragmentPath));

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader);
            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int success_v);
            if (success_v == 0) {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(FragmentShader);
            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int success_f);
            if (success_f == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(infoLog);
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success_h);
            if (success_h == 0) {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            setDefautlTransform();

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);
        }

        public int Use() {
            GL.UseProgram(Handle);
            return Handle;
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                GL.DeleteProgram(Handle);
                disposedValue = true;
            }
        }

        ~Shader() {
            if (disposedValue == false) {
                Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()???");
            }

        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        private void setDefautlTransform()
        {
            GL.UseProgram(Handle);
            Matrix4 scale = Matrix4.CreateScale(1.0f, 1.0f, 1.0f);
            int location = GL.GetUniformLocation(Handle, "transform");

            if (location != -1)
            {
                GL.UniformMatrix4(location, true, ref scale);
            }
        }
    }

}
