using ProgGrafica1.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.ejecutor.libreto
{
    public class Guion
    {
        public Queue<Escena> escenas { set; get; }

        public Guion(Queue<Escena> escenas = null) { 
            this.escenas = escenas?? new Queue<Escena>();
        }
        public void play() {
            foreach (var item in escenas)
                item.play();
        }
        public void cargarEscenas(Escenario escenario){
            foreach (var item in escenas)
                item.cargarAccion(escenario);
        }
    }
}
