using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_06
{

    class Day06Main
    {
        static void Main(string[] args)
        {
            //
            //string[] inputLines = File.ReadAllLines("sample.input");
            string[] inputLines = File.ReadAllLines("challenge.input");

            int[] times = GetIntsFromLine(inputLines[0]);
            int[] distances = GetIntsFromLine(inputLines[1]);
            int[] waysToWin = new int[times.Length];
            int totalWins = 1;
            int numRaces = times.Length;
            int distance = 0;

            //
            for (int i = 0; i < numRaces; i++) 
            {
                for (int j = 0; j < times[i]; j++)
                {
                    distance = j * (times[i] - j);
                    waysToWin[i] += (distance > distances[i]) ? 1 : 0;
                }
            }

            //
            for (int i = 0; i < numRaces; i++)
            {
                totalWins = totalWins * waysToWin[i];
            }

                // All done! Blat out the clossest location.
                //
                System.Console.WriteLine(totalWins);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }



        //
        static readonly Regex trimmer = new Regex(@"\s\s+");




        private static int[] GetIntsFromLine(string line)
        {
            //
            string[] tokens = trimmer.Replace(line, " ").Split(' ');
            int[] intValues = new int[tokens.Length - 1];

            //
            for (int i = 1; i < tokens.Length; i++)
            {
                intValues[i - 1] = int.Parse(tokens[i]);
            }

            return intValues;
        }


    }

}
