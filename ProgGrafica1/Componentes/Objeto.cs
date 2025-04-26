using OpenTK.Graphics.OpenGL4;
using ProgGrafica1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Objeto
    {
        public Transform transform { get; set; } = new Transform();
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
        public void draw(int program, Transform transform, Punto relativeOrigin) {
            Transform rtransform = Transform.combinarTransformacion(transform, this.transform);

            foreach (var elemento in elementos) elemento.Value.draw(program, rtransform, 
                ComponentUtils.sumarPuntos(relativeOrigin, this.position));
        }

        public void dispose() {
            foreach (var elemento in elementos) elemento.Value.dispose();
        }
    }
}
