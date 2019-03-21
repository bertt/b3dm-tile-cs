namespace B3dm.Tile
{
    public static class GlbToB3dmConvertor
    {
        public static B3dm Convert(byte[] glb)
        {
            var b3dm = new B3dm();
            b3dm.GlbData = glb;
            return b3dm;
        }
    }
}
