using OpenTK.Mathematics;
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

        public Matrix4 GetModelMatrix()
        {
            return Matrix4.CreateTranslation(Position) * Rotation * Matrix4.CreateScale(Scale);
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
