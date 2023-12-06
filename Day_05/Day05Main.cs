using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Day_05
{
    public class Day05Main
    {

        static void Main(string[] args)
        {
            // Functinal approch to create the state we will work with.
            string[] inputLines = File.ReadAllLines("input.txt");
            Queue<List<string>> inputChunks = GetChunks(inputLines);
            List<Int64> seeds = GetSeeds(inputChunks.Dequeue());
            List<Map> maps = GetMaps(inputChunks);

            // TODO: test to see if things actualy run faster with these declared here.
            Int64 current;
            Int64 closest = Int64.MaxValue;

            // HACK: Hacked this loop in for Part 2 to treat the seeds as a list of
            // ranges rather then induvidual seeds
            for (int seedIdx = 0; seedIdx < seeds.Count; seedIdx+=2)
            {
                // Dont really need this but the declerations here keep the loop initilisation readable
                // and the trace is nice as Part 2 runs for a good handul of minutes.
                Int64 seedStart = seeds[seedIdx];
                Int64 seedEnd = seeds[seedIdx] + seeds[seedIdx + 1];
                Console.WriteLine("Processing seeds: " + seedStart + "..." + seedEnd);

                // Loops for the current set of seeds (Previously all seeds in part 1).
                for (Int64 seed = seedStart; seed < seedEnd; seed++)
                {
                    //
                    current = seed;

                    //
                    foreach (Map map in maps)
                    {
                        current = map.LookUp(current);
                    }

                    //
                    if (current < closest)
                    {
                        closest = current;
                    }
                }
            }

            // All done! Blat out the clossest location.
            System.Console.WriteLine(closest);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Returns 'chunks' of lines from the given input. A chunk is continous block
        /// of non empty lines, empty lines are used to delinate chunks.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Assumes the given line contains a list of seeds which will be returned as a list of long integers.
        /// </summary>
        /// <param name="seedChunk"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Consumes chunks from the given input queue and constructs a map from each.
        /// All maps that have been constructed are returend in a single list in the
        /// order they are taken from the queue.
        /// </summary>
        /// <param name="inputChunks"></param>
        /// <returns></returns>
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

    /// <summary>
    /// Not really a map but acts like one from the out side. Inside it does a lot of simple
    /// maths to map large ranges to large ranges while holding minimal data.
    /// </summary>
    public class Map
    {
        private string name;
        int mapSegments = 0;
        private List<Int64> sources = new List<Int64>();
        private List<Int64> destinations = new List<Int64>();
        private List<Int64> lengths = new List<Int64>();

        /// <summary>
        /// Constructs a map from the given chunk of text.
        /// It is assumed that the given chunk describes ranges of mappings.
        /// </summary>
        /// <param name="seedChunk"></param>
        public Map(List<string> mapChunk) 
        {
            name = mapChunk[0];
            mapSegments = mapChunk.Count - 1;
            for (int i = 1; i < mapChunk.Count; i++)
            {
                string[] tokens = mapChunk[i].Split(' ');
                destinations.Add(Int64.Parse(tokens[0]));
                sources.Add(Int64.Parse(tokens[1]));
                lengths.Add(Int64.Parse(tokens[2]));
            }
        }

        /// <summary>
        /// Looks for a valid mapping in the ranges provided at construction.
        /// If found the mapping is returned, otherwise the input is returned.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
