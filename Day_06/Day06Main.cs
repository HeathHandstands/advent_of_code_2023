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

            UInt64[] times = GetIntsFromLineP2(inputLines[0]);
            UInt64[] distances = GetIntsFromLineP2(inputLines[1]);
            UInt64[] waysToWin = new UInt64[times.Length];
            UInt64 totalWins = 1;
            UInt64 numRaces = (UInt64)times.Length;
            UInt64 distance = 0;

            //
            for (UInt64 i = 0; i < numRaces; i++) 
            {
                for (UInt64 j = 0; j < times[i]; j++)
                {
                    distance = j * (times[i] - j);
                    if (distance > distances[i])
                    {
                        waysToWin[i]++;
                    }
                }
            }

            //
            for (UInt64 i = 0; i < numRaces; i++)
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




        private static int[] GetIntsFromLineP1(string line)
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


        private static UInt64[] GetIntsFromLineP2(string line)
        {
            //
            string[] tokens = line.Replace(" ", "").Split(':');
            UInt64[] intValues = new UInt64[tokens.Length - 1];

            //
            for (int i = 1; i < tokens.Length; i++)
            {
                intValues[i - 1] = UInt64.Parse(tokens[i]);
            }

            return intValues;
        }





    }

}
