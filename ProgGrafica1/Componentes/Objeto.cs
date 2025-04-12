using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Objeto
    {
        public List<Elemento> elementos { get; set; }
        public Punto position { get; set; }

        public Objeto(Punto position = null, List<Elemento> elementos = null) {
            this.position = position?? new Punto();
            this.elementos = elementos?? new List<Elemento>();
        }

        public void add( Elemento elemento)
        {
            elementos.Add(elemento);    
        }

        public void draw() {
            foreach (Elemento elemento in elementos) elemento.draw(position);
        }

        public void dispose() {
            foreach (Elemento elemento in elementos) elemento.dispose();
        }
    }
}
