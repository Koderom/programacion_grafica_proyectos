using OpenTK.Graphics.OpenGL4;
using ProgGrafica1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Elemento
    {
        public Dictionary<String, Cara> caras { get; set; }
        public Punto position { get; set; }

        public Elemento(Punto position = null, Dictionary<String, Cara> caras = null)
        {
            this.position = position?? new Punto();
            this.caras = caras ?? new Dictionary<String, Cara>();
        }

        public void add(String id, Cara cara)
        {
            caras.Add(id, cara);
        }

        public Cara GetCara(String id) { 
            return caras[id];
        }
        public void draw(Punto relativeOrigin)
        {
            foreach (var cara in caras) cara.Value.draw( 
                ComponentUtils.sumarPuntos(relativeOrigin, this.position) 
            );
        }

        public void dispose()
        {
            foreach (var cara in caras) cara.Value.dispose();
        }
    }
}
