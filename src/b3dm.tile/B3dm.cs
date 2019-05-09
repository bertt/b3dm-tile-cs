namespace B3dm.Tile
{
    public class B3dm
    {
        public B3dm()
        {
            B3dmHeader = new B3dmHeader();
            FeatureTableJson = string.Empty;
            BatchTableJson = string.Empty;
        }

        public B3dm(byte[] glb)
        {
            GlbData = glb;
            B3dmHeader = new B3dmHeader();
            FeatureTableJson = string.Empty;
            BatchTableJson = string.Empty;
        }

        public B3dmHeader B3dmHeader { get; set; }
        public string FeatureTableJson { get; set; }
        public byte[] FeatureTableBinary { get; set; }
        public string BatchTableJson { get; set; }
        public byte[] BatchTableBinary { get; set; }
        public byte[] GlbData { get; set; }
    }
}
