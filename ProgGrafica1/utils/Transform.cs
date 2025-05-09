using OpenTK.Mathematics;
using ProgGrafica1.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGrafica1.utils
{
    public class Transform
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Scale = Vector3.One;
        public Matrix4 Rotation = Matrix4.Identity;

        public Matrix4 GetModelMatrix(Punto punto)
        {
            Matrix4 model = Matrix4.Identity;
            model *= Matrix4.CreateTranslation(-punto.x, -punto.y, -punto.z);
            model *= Rotation;
            model *= Matrix4.CreateTranslation(punto.x, punto.y, punto.z);
            model *= Matrix4.CreateTranslation(Position);
            model *= Matrix4.CreateScale(Scale);

            return model;
        }

        public static Transform combinarTransformacion(Transform a, Transform b){
            a = a?? new Transform();
            b = b?? new Transform();

            Transform result = new Transform();
            result.Position.X = a.Position.X + b.Position.X;
            result.Position.Y = a.Position.Y + b.Position.Y;
            result.Position.Z = a.Position.Z + b.Position.Z;

            result.Scale = a.Scale * b.Scale;
            result.Rotation = a.Rotation * b.Rotation;

            return result;
        }
    }
}
