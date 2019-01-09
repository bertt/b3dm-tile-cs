namespace B3dm.Tile
{
    public class B3dm
    {
        public string Magic { get; set; }
        public int Version {get; set; }
        public byte[] GlbData { get; set; }
        public int FeatureTableJsonByteLength { get; set; }
        public int FeatureTableBinaryByteLength { get; set; }
        public int BatchTableJsonByteLength { get; set; }
        public int BatchTableBinaryByteLength { get; set; }
    }
}
