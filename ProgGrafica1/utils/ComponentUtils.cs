using ProgGrafica1.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProgGrafica1.utils
{
    public class ComponentUtils
    {
        public static List<Punto> fixToAbsoluteOrigin(Punto absoluteOrigin, List<Punto> relativesPoints)
        {
            List<Punto> fixedPoints = new List<Punto>();

            foreach (Punto points in relativesPoints)
            {
                fixedPoints.Add(
                    new Punto() {
                        x = points.x + absoluteOrigin.x,
                        y = points.y + absoluteOrigin.y,
                        z = points.z + absoluteOrigin.z
                    }
                );    
            }

            return fixedPoints;
        }
        public static float[] pointListToArray( List<Punto>  pointList) {
            List<float> pointArray = new List<float>();

            foreach (Punto item in pointList)
            {
                pointArray.Add(item.x);
                pointArray.Add(item.y);
                pointArray.Add(item.z);
            }

            return pointArray.ToArray();
        }
        public static Punto sumarPuntos(Punto a, Punto b) {
        {
            return new Punto(
                x: a.x + b.x,
                y: a.y + b.y,
                z: a.z + b.z
            );
        }
    }
        public static void saveData<T> (String path, T data)
        {
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDir, path);

            string jsonEscenario = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonEscenario);
        }
        public static T loadData<T>(String path)
        {
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDir, path);

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
