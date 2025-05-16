using ProgGrafica1.Elements;
using ProgGrafica1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.ejecutor.libreto
{
    public class Escena
    {
        public Queue<Accion> acciones { set; get; }
        public String objeto { set; get; }

        public Escena(Queue<Accion> acciones = null, String objeto = null) { 
            this.acciones = acciones ?? new Queue<Accion>();
            this.objeto = objeto;
        }

        public void playNext() {
            foreach (Accion acc in acciones) { 
                acc.play(); 
            }                      
        }
        public void cargarAccion(Escenario escenario) {
            if (objeto == null) return;
            foreach (var item in acciones)
            {
                if (escenario.objetos.ContainsKey(objeto))
                    item.objeto = escenario.getObject(objeto);
                else item.objeto = null;
            }
        }
    }
}
