namespace pg2b3dm
{

    public class TileSet
    {
        public Root root { get; set; }
        public int geometricError { get; set; }
        public Asset asset { get; set; }
    }

    public class Root
    {
        public Child[] children { get; set; }
        public float[] transform { get; set; }
        public int geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume boundingVolume { get; set; }
    }

    public class Boundingvolume
    {
        public float[] box { get; set; }
    }

    public class Child
    {
        public Child1[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume1 boundingVolume { get; set; }
        public Content content { get; set; }
    }

    public class Boundingvolume1
    {
        public float[] box { get; set; }
    }

    public class Content
    {
        public string uri { get; set; }
    }

    public class Child1
    {
        public Child2[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume2 boundingVolume { get; set; }
        public Content1 content { get; set; }
    }

    public class Boundingvolume2
    {
        public float[] box { get; set; }
    }

    public class Content1
    {
        public string uri { get; set; }
    }

    public class Child2
    {
        public Child3[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume3 boundingVolume { get; set; }
        public Content2 content { get; set; }
    }

    public class Boundingvolume3
    {
        public float[] box { get; set; }
    }

    public class Content2
    {
        public string uri { get; set; }
    }

    public class Child3
    {
        public Child4[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume4 boundingVolume { get; set; }
        public Content3 content { get; set; }
    }

    public class Boundingvolume4
    {
        public float[] box { get; set; }
    }

    public class Content3
    {
        public string uri { get; set; }
    }

    public class Child4
    {
        public Child5[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume5 boundingVolume { get; set; }
        public Content4 content { get; set; }
    }

    public class Boundingvolume5
    {
        public float[] box { get; set; }
    }

    public class Content4
    {
        public string uri { get; set; }
    }

    public class Child5
    {
        public Child6[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume6 boundingVolume { get; set; }
        public Content5 content { get; set; }
    }

    public class Boundingvolume6
    {
        public float[] box { get; set; }
    }

    public class Content5
    {
        public string uri { get; set; }
    }

    public class Child6
    {
        public Child7[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume7 boundingVolume { get; set; }
        public Content6 content { get; set; }
    }

    public class Boundingvolume7
    {
        public float[] box { get; set; }
    }

    public class Content6
    {
        public string uri { get; set; }
    }

    public class Child7
    {
        public object[] children { get; set; }
        public float geometricError { get; set; }
        public string refine { get; set; }
        public Boundingvolume8 boundingVolume { get; set; }
        public Content7 content { get; set; }
    }

    public class Boundingvolume8
    {
        public float[] box { get; set; }
    }

    public class Content7
    {
        public string uri { get; set; }
    }

    public class Asset
    {
        public string version { get; set; }
    }


}
