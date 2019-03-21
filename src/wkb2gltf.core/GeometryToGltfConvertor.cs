using System.IO;
using glTFLoader;
using Wkx;

namespace Wkb2Gltf
{
    public static class GeometryToGlbConvertor
    {
        public static byte[] Convert(Geometry g, float[] translation)
        {
            var polyhedralsurface = ((PolyhedralSurface)g);
            var center = polyhedralsurface.GetCenter();
            var gltf = Gltf2Loader.GetGltf(polyhedralsurface, translation);
            var ms = new MemoryStream();
            gltf.Gltf.SaveBinaryModel(gltf.Body, ms);
            return ms.ToArray();
        }
    }
}

