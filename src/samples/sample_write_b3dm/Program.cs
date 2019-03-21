using System;
using System.IO;
using B3dm.Tile;

namespace sample_write_b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputfile= @"testfixtures/building.b3dm";
            var outputfile = Path.GetFileNameWithoutExtension(inputfile) + "_new.b3dm";
            var f = File.OpenRead(inputfile);
            var b3dm = B3dmReader.ReadB3dm(f);
            B3dmWriter.WriteB3dm(outputfile, b3dm);
            Console.WriteLine("File created: " + outputfile);
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }
    }
}
