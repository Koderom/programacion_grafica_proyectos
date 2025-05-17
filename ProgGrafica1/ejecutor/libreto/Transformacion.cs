using OpenTK.Mathematics;
using ProgGrafica1.Elements;
using ProgGrafica1.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ProgGrafica1.ejecutor.libreto
{
    public class Transformacion : ICloneable
    {
        public double tInicio { get; set; }
        public double tFinal { get; set; }
        public float[] rotacion { get; set; }
        public float[] traslacion { get; set; }
        public float escala { get; set; }

        private double frames = 0;

        public Transformacion(
            float[] rotacion = null, float[] traslacion = null, float escala = 1, 
            double tInicio = 0, double tFinal = 0) { 

            this.rotacion = rotacion?? new float[3] { 0,0,0};
            this.traslacion = traslacion?? new float[3] {0,0,0};
            this.escala = escala; 
            this.tInicio = tInicio;
            this.tFinal = tFinal;
        }

        public void aplicar(Objeto objeto) {
            Cronometro cronometro = Cronometro.getInstance();

            if (this.tInicio < cronometro.getTime() &&  this.tInicio <= this.tFinal) {
                double deltaTime = cronometro.getTime() - this.tInicio;
                double deltaFrames = cronometro.getFrames() - this.frames;
                double fps = deltaFrames / deltaTime;

                double tFrames = fps * (tFinal - tInicio);
                Transformacion cTransformacion = (Transformacion) this.Clone();

                //Logica para aplicar la traslacion
                if (cTransformacion.traslacion[0] != 0) cTransformacion.traslacion[0] *= (float)(deltaFrames / tFrames);
                if (cTransformacion.traslacion[1] != 0) cTransformacion.traslacion[1] *= (float)(deltaFrames / tFrames);
                if (cTransformacion.traslacion[2] != 0) cTransformacion.traslacion[2] *= (float)(deltaFrames / tFrames);

                //Logica para aplicar la rotacion
                if (cTransformacion.rotacion[0] != 0) cTransformacion.rotacion[0] *= (float)(deltaFrames / tFrames);
                if (cTransformacion.rotacion[1] != 0) cTransformacion.rotacion[1] *= (float)(deltaFrames / tFrames);
                if (cTransformacion.rotacion[2] != 0) cTransformacion.rotacion[2] *= (float)(deltaFrames / tFrames);

                //Logica para aplicar escala
                double escalaAplicable = Math.Abs(1 - cTransformacion.escala);
                if (escalaAplicable > 0) {
                    cTransformacion.escala = (float)Math.Pow(cTransformacion.escala, 1 / tFrames);
                    this.escala = (float)Math.Pow(cTransformacion.escala, tFrames - 1);
                }

                objeto.transform = Transform.combinarTransformacion(
                    objeto.transform, cTransformacion.getModelTransform());

                this.traslacion[0] += cTransformacion.traslacion[0] * -1;
                this.traslacion[1] += cTransformacion.traslacion[1] * -1;
                this.traslacion[2] += cTransformacion.traslacion[2] * -1;

                this.rotacion[0] += cTransformacion.rotacion[0] * -1;
                this.rotacion[1] += cTransformacion.rotacion[1] * -1;
                this.rotacion[2] += cTransformacion.rotacion[2] * -1;

                this.tInicio = cronometro.getTime();
                this.frames = cronometro.getFrames();
            }
        }

        public Vector3 getEscala() {
            return new Vector3(escala);
        }
        public Vector3 getTraslacion() {
            return new Vector3(traslacion[0], traslacion[1], traslacion[2]);
        }

        public Matrix4 getRotacion() { 
            Matrix4 tRotacion = Matrix4.Identity;
            float degreess = 0;

            degreess = MathHelper.DegreesToRadians(rotacion[0]);
            tRotacion *= Matrix4.CreateRotationX(degreess);
            degreess = MathHelper.DegreesToRadians(rotacion[1]);
            tRotacion *= Matrix4.CreateRotationY(degreess);
            degreess = MathHelper.DegreesToRadians(rotacion[2]);
            tRotacion *= Matrix4.CreateRotationZ(degreess);

            return tRotacion;
        }

        public Transform getModelTransform() { 
            Transform transform = new Transform();  
            transform.Scale = getEscala();
            transform.Position = getTraslacion();
            transform.Rotation = getRotacion();

            return transform;
        }

        public Matrix4 GetModelMatrix()
        {
            Matrix4 model = Matrix4.Identity;

            Matrix4 tRotacion = getRotacion();
            //model *= Matrix4.CreateTranslation(-punto.x, -punto.y, -punto.z);
            model *= tRotacion;
            //model *= Matrix4.CreateTranslation(punto.x, punto.y, punto.z);
            model *= Matrix4.CreateTranslation(getTraslacion() );
            model *= Matrix4.CreateScale(getEscala());

            return model;
        }

        public Boolean isAplicada()
        {
            Boolean isTraslacionAplicada =
                this.traslacion[0] +
                this.traslacion[1] +
                this.traslacion[2] == 0.0;

            Boolean isRotacionAplicada =
                this.rotacion[0] +
                this.rotacion[1] +
                this.rotacion[2] == 0.0;

            Boolean isEscalaAplicada = this.escala == 1.0;

            Boolean isTiempoTerminado = tFinal < tInicio;

            return (isTraslacionAplicada && isRotacionAplicada && isEscalaAplicada) || isTiempoTerminado;
        }

        public object Clone()
        {
            return new Transformacion(
                tInicio: this.tInicio,
                tFinal: this.tFinal,
                traslacion: new float[] { this.traslacion[0], this.traslacion[1], this.traslacion[2] },
                rotacion: new float[] { this.rotacion[0], this.rotacion[1], this.rotacion[2] },
                escala: this.escala
            );
        }
    }
}
