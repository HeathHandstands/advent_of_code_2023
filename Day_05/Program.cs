using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Day_05
{
    public class Program
    {

        static void Main(string[] args)
        {
            //
            string[] inputLines = File.ReadAllLines("input.txt");
            Queue<List<string>> inputChunks = GetChunks(inputLines);
            List<Int64> seeds = GetSeeds(inputChunks.Dequeue());
            List<Map> maps = GetMaps(inputChunks);


            //
            

            //
            Int64 current;
            Int64 mapping;
            Int64 closest = Int64.MaxValue;

            //
            for (int seedIdx = 0; seedIdx < seeds.Count; seedIdx+=2)
            {
                Int64 seedStart = seeds[seedIdx];
                Int64 seedEnd = seeds[seedIdx] + seeds[seedIdx + 1];

                Console.WriteLine("Processing " + seedStart + "..." + seedEnd);

                for (Int64 seed = seedStart; seed < seedEnd; seed++)
                {

                    current = seed;
                    foreach (Map map in maps)
                    {
                        current = map.LookUp(current);
                    }

                    if (current < closest)
                    {
                        closest = current;
                    }

                }
            }

            //
            System.Console.WriteLine(closest);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        //
        private static Queue<List<string>> GetChunks(string[] lines)
        {
            Queue<List<string>> chunks = new Queue<List<string>>();

            List<string> currentChunk = new List<string>();
            foreach (string line in  lines)
            {
                if (line == "")
                {
                    chunks.Enqueue(currentChunk);
                    currentChunk = new List<string>();
                }
                else
                {
                    currentChunk.Add(line);
                }
            }

            if (currentChunk.Count > 0) 
            {
                chunks.Enqueue(currentChunk); 
            }

            return chunks;
        }

        //
        private static List<Int64> GetSeeds(List<string> seedChunk)
        {
            List<Int64> seeds = new List<Int64>();

            string[] tokens = seedChunk[0].Split(' ');
            for (int i = 1; i < tokens.Length; i++)
            {
                seeds.Add(Int64.Parse(tokens[i]));
            }

            return seeds;
        }

        //
        public static List<Map> GetMaps(Queue<List<string>> inputChunks)
        {
            List <Map> maps = new List<Map>();

            while (inputChunks.Count > 0)
            {
                maps.Add(new Map(inputChunks.Dequeue()));
            }

            return maps;
        }

    }



    public class Map
    {
        private string name;
        int mapSegments = 0;
        private List<Int64> sources = new List<Int64>();
        private List<Int64> destinations = new List<Int64>();
        private List<Int64> lengths = new List<Int64>();

        public Map(List<string> seedChunk) 
        {
            name = seedChunk[0];
            mapSegments = seedChunk.Count - 1;
            for (int i = 1; i < seedChunk.Count; i++)
            {
                string[] tokens = seedChunk[i].Split(' ');
                destinations.Add(Int64.Parse(tokens[0]));
                sources.Add(Int64.Parse(tokens[1]));
                lengths.Add(Int64.Parse(tokens[2]));
            }
        }

        public Int64 LookUp(Int64 key)
        {
            for (int i = 0; i < mapSegments; i ++ )
            {
                if (key >= sources[i] && key < sources[i] + lengths[i])
                {
                    Int64 diff = key - sources[i];
                    Int64 retVal = destinations[i] + diff;
                    return retVal;
                }
            }

            return key;
        }

    }

}
