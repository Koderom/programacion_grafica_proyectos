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
        public int indexTransformacion { get; set; }
        public Accion(List<Transformacion> transformaciones = null, Objeto objeto = null, bool isLoop = false, int indexTransformacion = 0)
        {
            this.transformaciones = transformaciones ?? new List<Transformacion>();
            this.objeto = objeto;
            this.isLoop = isLoop;

            this.indexTransformacion = indexTransformacion;
        }

        public void play() {
            if(indexTransformacion >= transformaciones.Count) indexTransformacion = 0;

            if (objeto != null && transformaciones.Count > indexTransformacion) {
                Transformacion transformacion = transformaciones[indexTransformacion];
                transformacion.aplicar(objeto);

                indexTransformacion++;
                //if (transformacion.isAplicada()) indexTransformacion++;
            }
        }

        
    }
}
