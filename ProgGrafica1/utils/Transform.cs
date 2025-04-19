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
    }
}
