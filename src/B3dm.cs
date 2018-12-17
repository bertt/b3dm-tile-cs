namespace B3dm.Tile
{
    public class B3dm
    {
        public string Magic { get; set; }
        public int Version {get; set; }
        public byte[] GlbData { get; set; }
        public Glb Glb { get; set; }
    }
}
