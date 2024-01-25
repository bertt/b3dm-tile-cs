using System;
using System.IO;
using System.Linq;
using System.Text;

namespace B3dmCore;

public static class B3dmReader
{
    public static B3dm ReadB3dm(BinaryReader reader)
    {
        var b3dmHeader = new B3dmHeader(reader);
        var featureTableJson = Encoding.UTF8.GetString(reader.ReadBytes(b3dmHeader.FeatureTableJsonByteLength));
        var featureTableBytes = reader.ReadBytes(b3dmHeader.FeatureTableBinaryByteLength);
        var batchTableJson = Encoding.UTF8.GetString(reader.ReadBytes(b3dmHeader.BatchTableJsonByteLength));
        var batchTableBytes = reader.ReadBytes(b3dmHeader.BatchTableBinaryByteLength);

        // the rest of the file is the glb
        var glbMaxLength = b3dmHeader.ByteLength - b3dmHeader.Length;
        var glbBuffer = reader.ReadBytes(glbMaxLength);
        // but we get the length from the glb itself
        var glbLength = BitConverter.ToInt32(glbBuffer, 8);

        // if the glb is shorter than the expected length, we need to trim the buffer
        if(glbLength < glbMaxLength) {
            glbBuffer = glbBuffer.Take(glbLength).ToArray();
        }

        var b3dm = new B3dm {
            B3dmHeader = b3dmHeader,
            GlbData = glbBuffer,
            FeatureTableJson = featureTableJson,
            FeatureTableBinary = featureTableBytes,
            BatchTableJson = batchTableJson,
            BatchTableBinary = batchTableBytes
        };
        return b3dm;
    }

    public static B3dm ReadB3dm(Stream stream)
    {
        using (var reader = new BinaryReader(stream)) {
            var b3dm = ReadB3dm(reader);
            return b3dm;
        }
    }
}