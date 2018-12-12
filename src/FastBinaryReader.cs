using System;
using System.IO;
using System.Runtime.InteropServices;

namespace B3dm.Tile
{
    public unsafe class FastBinaryReader : IDisposable
    {
        private byte[] data;
        private long position;

        private GCHandle handle;
        private byte* fixedPtr;

        public FastBinaryReader(Stream cStream)
        {
            var ms = new MemoryStream();
            cStream.CopyTo(ms);
            data = ms.ToArray();
            ms.Dispose();
            handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            fixedPtr = (byte*)handle.AddrOfPinnedObject().ToPointer();
        }

        public bool HasMore()
        {
            return position < data.Length;
        }

        public byte ReadByte()
        {
            byte ret = *fixedPtr;
            position++;
            fixedPtr = fixedPtr + 1;
            return ret;
        }

        public byte[] ReadBytes(uint length)
        {
            var bytes = new byte[length];
            for(int i = 0; i < length; i += 1)
            {
                bytes[i] = ReadByte();
            }
            return bytes;
       }

        public float ReadSingle()
        {
            float ret = *(float*)fixedPtr;
            fixedPtr = fixedPtr + 4;
            position = position + 4;
            return ret;
        }

        public double ReadDouble()
        {
            double ret = *(double*)fixedPtr;
            fixedPtr = fixedPtr + 8;
            position = position + 8;
            return ret;
        }

        public ushort ReadUInt16()
        {
            ushort ret = *(ushort*)fixedPtr;
            fixedPtr = fixedPtr + 2;
            position = position + 2;
            return ret;
        }

        public uint ReadUInt32()
        {
            uint ret = *(uint*)fixedPtr;
            fixedPtr = fixedPtr + 4;
            position = position + 4;
            return ret;
        }

        public void Dispose()
        {
            handle.Free();
            fixedPtr = null;

            data = null;
        }
    }
}
