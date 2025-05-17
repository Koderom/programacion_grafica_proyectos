using ProgGrafica1.ejecutor.libreto;
using ProgGrafica1.Elements;
using ProgGrafica1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgGrafica1.ejecutor
{
    public class Ejecutor
    {
        public Escenario escenario { get; set;}
        private Guion guion;
        private Thread hilo;

        public Ejecutor(Escenario escenario)
        {
            this.escenario = escenario;
            hilo = new Thread(run);
        }

        public void play() {
            if (guion == null) cargarGuion();

            if (!hilo.IsAlive && escenario != null) 
                hilo.Start();
        }

        private void run() {
            while (true) {
                lock (escenario) {
                    guion.play();
                }
            } 
        }

        private void cargarGuion() {
            if (escenario == null) return;
            guion = ComponentUtils.loadData<Guion>("./assets/guion.json");
            guion.cargarEscenas(escenario);
        }

        
    }
}
