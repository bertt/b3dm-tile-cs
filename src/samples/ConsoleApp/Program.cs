using System;
using System.IO;
using System.Numerics;
using B3dm.Tile;
using DotSpatial.Positioning;

namespace ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            string infile = "testfixtures/29.b3dm";
            string outfile = "29.glb";

            // extracted from tileset.json (copied from http://saturnus.geodan.nl/tomt/data/buildingtiles_oudeschild/tileset.json)
            double[] tilesetJsonTransform = { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 3830058.036, 324388.491, 5072788.606, 1 };

            // from https://github.com/AnalyticalGraphicsInc/3d-tiles/tree/master/specification#box
            //"The boundingVolume.box property is an array of 12 numbers that define an oriented bounding box in a right-handed 3 - axis(x, y, z) Cartesian coordinate system 
            //where the z-axis is up.The first three elements define the x, y, and z values for the center of the box.The next three elements(with indices 3, 4, and 5) 
            //define the x - axis direction and half - length.The next three elements(indices 6, 7, and 8) define the y - axis direction and half - length.
            //The last three elements(indices 9, 10, and 11) define the z - axis direction and half - length."
            // so first three: center of box, then y-axis direction + half length + z-axis direction + half length
            double[] tile_boundingVolume = { -85.132, 86.914, 58.116, 69.763, 0, 0, 0, 61.386, 0, 0, 0, 59.127 };


            // from https://github.com/AnalyticalGraphicsInc/3d-tiles/tree/master/specification#box
            // "The geometricError property is a nonnegative number that defines the error, in meters, introduced if this tile is rendered and its children are not. 
            // At runtime, the geometric error is used to compute Screen-Space Error(SSE), the error measured in pixels.The SSE determines if a tile is sufficiently 
            // detailed for the current view or if its children should be considered, see Geometric error."
            // double tilegeometricError = 7.8125;

            var rtc_cartesian = GetCartesianPoint((float)tilesetJsonTransform[12], (float)tilesetJsonTransform[13], (float)tilesetJsonTransform[14]);
            var tile_cartesian = GetCartesianPoint((float)tile_boundingVolume[0], (float)tile_boundingVolume[1], (float)tile_boundingVolume[2]);
            // q: is rotation directions correct or not?
            var rotation_x = tile_boundingVolume[3];
            var rotation_y = tile_boundingVolume[7];
            var rotation_z = tile_boundingVolume[11];

            // process rotations
            //var rotationVector = new Vector3((float)rotation_x, (float)rotation_y, (float)rotation_z);

            var centerPosition = (rtc_cartesian + tile_cartesian).ToPosition3D();
            Console.WriteLine($"Center tile: {centerPosition.Longitude.DecimalDegrees}, {centerPosition.Latitude.DecimalDegrees}, {centerPosition.Altitude.Value}");
            var stream =File.OpenRead(infile);
            Console.WriteLine("B3dm tile sample application");
            Console.WriteLine($"Start parsing {infile}...");
            var b3dm = B3dmReader.ReadB3dm(stream);
            Console.WriteLine($"Start writing output to {outfile}.");

            var fs = File.Create(outfile);
            var bw = new BinaryWriter(fs);
            bw.Write(b3dm.GlbData);
            bw.Close();

            var gltfVersion = GltfVersionChecker.GetGlbVersion(b3dm.GlbData);

            // sample: load in gltf loader
            var model = glTFLoader.Interface.LoadModel(new MemoryStream(b3dm.GlbData));

            Console.WriteLine("Generator: " + model.Asset.Generator);

            Console.WriteLine($"Gltf version: {gltfVersion}");
            Console.WriteLine($"Press any key to continue...");
            Console.ReadKey();
        }


        private static CartesianPoint GetCartesianPoint(float x, float y, float z)
        {
            var distance_x = new Distance(x, DistanceUnit.Meters);
            var distance_y = new Distance(y, DistanceUnit.Meters);
            var distance_z = new Distance(z, DistanceUnit.Meters);

            var cartesian = new CartesianPoint(distance_x, distance_y, distance_z);
            return cartesian;
        }

    }
}
