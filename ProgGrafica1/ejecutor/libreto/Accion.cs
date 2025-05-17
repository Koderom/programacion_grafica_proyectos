using OpenTK.Mathematics;
using ProgGrafica1.Elements;
using ProgGrafica1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.ejecutor.libreto
{
    public class Accion
    {
        public Boolean isLoop {  get; set; }
        public Objeto objeto { get; set; }
        public List<Transformacion> transformaciones { get; set; }

        private List<Transformacion> modelTransformaciones;
        public Accion(List<Transformacion> transformaciones = null, Objeto objeto = null, bool isLoop = false)
        {
            this.transformaciones = transformaciones ?? new List<Transformacion>();
            this.objeto = objeto;
            this.isLoop = isLoop;

            this.modelTransformaciones = getCloneTransformaciones(this.transformaciones);
        }

        public void play() {
            foreach (Transformacion transform in transformaciones) {
                transform.aplicar(objeto);
            }
        }

        public Boolean isAccionFinalizada() {
            foreach (Transformacion transform in transformaciones)
                if(transform.isAplicada() == false) return false;
            
            return true;
        }

        public Accion getAccionLoop() {
            List<Transformacion> model = getCloneTransformaciones(modelTransformaciones);
            double tbase = Cronometro.getInstance().getTime();
            foreach (Transformacion item in model)
            {
                double duracion = item.tFinal - item.tInicio;
                item.tInicio = item.tFinal;
                item.tFinal = item.tFinal + duracion;
            }

            return new Accion(transformaciones: model, objeto: objeto, isLoop: isLoop);
        }
        private List<Transformacion> getCloneTransformaciones(List<Transformacion> transformaciones) {
            List<Transformacion> model = new List<Transformacion> ();
            foreach (Transformacion transformacion in transformaciones) { 
                model.Add((Transformacion) transformacion.Clone());
            }
            return model;
        }
    }
}
