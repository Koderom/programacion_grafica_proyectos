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
        public List<Cara> caras { get; set; }
        public Punto position { get; set; }

        public Elemento(Punto position = null, List<Cara> caras = null)
        {
            this.position = position?? new Punto();
            this.caras = caras ?? new List<Cara>();
        }

        public void add(Cara cara)
        {
            caras.Add(cara);
        }

        public void draw(Punto relativeOrigin)
        {
            foreach (Cara cara in caras) cara.draw( 
                ComponentUtils.sumarPuntos(relativeOrigin, this.position) 
            );
        }

        public void dispose()
        {
            foreach (Cara cara in caras) cara.dispose();
        }
    }
}
