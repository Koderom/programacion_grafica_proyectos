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
        int VAO;

        public List<Elemento> elementos { get; set; }
        public Punto position { get; set; }

        public Objeto(Punto position, List<Elemento> elementos = null) {
            this.position = position;
            this.elementos = elementos?? new List<Elemento>();

            VAO = GL.GenVertexArray();
        }

        public void add( Elemento elemento)
        {
            elementos.Add(elemento);    
        }

        public void draw() {
            GL.BindVertexArray(VAO);
            foreach (Elemento elemento in elementos) {
                elemento.draw(position);

                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
            } 

            GL.BindVertexArray(0);
        }

        public void dispose() {
            GL.DeleteVertexArray(VAO);
            foreach (Elemento elemento in elementos) elemento.dispose();
        }
    }
}
