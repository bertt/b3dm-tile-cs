using System.IO;
using glTFLoader;
using Wkx;

namespace Wkb2Gltf
{
    public static class GeometryToGlbConvertor
    {
        public static byte[] Convert(Geometry g, double[] translation)
        {
            var gltf = Gltf2Loader.GetGltf((PolyhedralSurface)g, translation);
            var ms = new MemoryStream();
            gltf.Gltf.SaveBinaryModel(gltf.Body, ms);
            return ms.ToArray();
        }
    }
}

