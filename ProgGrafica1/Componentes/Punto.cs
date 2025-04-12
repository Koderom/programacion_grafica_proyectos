using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Punto : ICloneable
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public Punto(float x = 0, float y = 0, float z = 0) { 
            this.x = x; 
            this.y = y;
            this.z = z;
        }

        public float[] ToArray() { 
            return new float[]{ x,y,z };
        }

        public static Punto calculateResultPoint( Punto a, Punto b) {
            return new Punto(
                x: a.x + b.x,
                y: a.y + b.y,
                z: a.z + b.z
            );
        }

        public object Clone()
        {
            return new Punto { x = this.x, y = this.y, z = this.z };
        }
    }
}
