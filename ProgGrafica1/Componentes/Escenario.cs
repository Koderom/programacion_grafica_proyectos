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
        public Dictionary<String,Objeto> objetos { get; set; } = new Dictionary<String, Objeto>();

        public Escenario(Dictionary<String, Objeto> objetos = null) 
        {
            this.objetos = objetos?? new Dictionary<String, Objeto>();
        }

        public void addObjeto(String id, Objeto objeto)
        {
            objetos.Add(id, objeto);
        }

        public void draw() 
        {
            foreach( var objeto in objetos) objeto.Value.draw();
            
        }

        public Objeto getObject(String id) 
        {
            return objetos[id];
        }

        public void dispose()
        {
            foreach (var objeto in objetos) objeto.Value.dispose();
        }
    }
}
