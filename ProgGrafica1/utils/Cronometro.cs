using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.utils
{
    public class Cronometro
    {
        private static readonly object _lock = new object();
        private static Cronometro instance;

        private double time;
        private double fps;
        private double frames;

        private Cronometro()
        {
            this.time = 0;
            this.fps = 60;
            this.frames = 0;
        }

        public static Cronometro getInstance()
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new Cronometro();
                }
                return instance;
            }
        }

        public void setParams(double time, double frames) {
            lock (_lock) {
                this.fps += frames / time;
                this.time += time;
                this.frames += frames;
            }
        }

        public double getTime()
        {
            lock (_lock)
            {
                return this.time;
            }
        }

        public double getFps()
        {
            lock (_lock)
            {
                return this.fps;
            }
        }
        public double getFrames()
        {
            lock (_lock)
            {
                return this.frames;
            }
        }
    }
}
