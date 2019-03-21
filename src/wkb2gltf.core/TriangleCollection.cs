using System.Collections.Generic;
using System.Numerics;

namespace Wkb2Gltf.Core
{
    public class TriangleCollection: List<Triangle>
    {
        public byte[] PositionsToBinary()
        {
            var floats = new List<float>();
            foreach (var triangle in this)
            {
                floats.AddRange(triangle.Flatten());
            }
            var bytes = BinaryConvertor.ToBinary(floats.ToArray());
            return bytes;
        }

        public List<Vector3> GetNormals()
        {
            var normals = new List<Vector3>();
            foreach (var triangle in this)
            {
                normals.Add(triangle.GetNormal());
            }
            return normals;
        }

        private List<Vector3> GetFaces(List<Vector3> normals)
        {
            var faces = new List<Vector3>();

            foreach (var normal in normals)
            {
                // heuh???
                faces.Add(normal);
                faces.Add(normal);
                faces.Add(normal);
            }

            return faces;
        }

        public byte[] NormalsToBinary()
        {
            var normals = GetNormals();
            var faces = GetFaces(normals);

            var floats = new List<float>();
            foreach(var face in faces)
            {
                floats.Add(face.X);
                floats.Add(face.Y);
                floats.Add(face.Z);
            }
            var bytes = BinaryConvertor.ToBinary(floats.ToArray());
            return bytes;
        }
    }
}
