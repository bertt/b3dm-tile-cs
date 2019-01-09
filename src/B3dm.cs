namespace B3dm.Tile
{
    public class B3dm
    {
        public string Magic { get; set; }
        public int Version {get; set; }
        public byte[] GlbData { get; set; }
        public string FeatureTableJson { get; set; }
        public byte[] FeatureTableBinary { get; set; }
        public string BatchTableJson { get; set; }
        public byte[] BatchTableBinary { get; set; }
    }
}
