using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.Elements
{
    public class Escenario
    {
        public List<Objeto> objetos { get; set; } = new List<Objeto>();

        public Escenario(List<Objeto> objetos = null) 
        {
            this.objetos = objetos?? new List<Objeto>();
        }

        public void addObjeto(Objeto objeto)
        {
            objetos.Add(objeto);
        }

        public void draw() 
        {
            foreach( Objeto objeto in objetos) objeto.draw();
            
        }

        public void dispose()
        {
            foreach (Objeto objeto in objetos) objeto.dispose();
        }
    }
}
