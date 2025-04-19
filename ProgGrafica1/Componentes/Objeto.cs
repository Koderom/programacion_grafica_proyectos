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
        public Dictionary<String, Elemento> elementos { get; set; }
        public Punto position { get; set; }

        public Objeto(Punto position = null, Dictionary<String, Elemento> elementos = null) {
            this.position = position?? new Punto();
            this.elementos = elementos?? new Dictionary<String, Elemento>();
        }

        public void add(String id, Elemento elemento)
        {
            elementos.Add(id, elemento);    
        }
        public Elemento getElemento(String id)
        {
            return elementos[id];
        }
        public void draw() {
            foreach (var elemento in elementos) elemento.Value.draw(position);
        }

        public void dispose() {
            foreach (var elemento in elementos) elemento.Value.dispose();
        }
    }
}
