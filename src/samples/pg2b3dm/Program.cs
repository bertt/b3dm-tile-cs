using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace pg2b3dm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("tool: pg2b3dm");
            Console.WriteLine("version: alpha alpha alpha");

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration["connectionstring"];
            var table = configuration["geometry_table"];
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var cmd = new NpgsqlCommand("SELECT * from tmp.tmp", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Console.WriteLine(reader.GetInt64(0));
            };
            conn.Close();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
