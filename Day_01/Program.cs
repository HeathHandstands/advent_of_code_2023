using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Day_01
{
    /// <summary>
    /// 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entrypoint for the day 1 chalange.
        /// </summary>
        /// <param name="args">Currently not used.</param>
        static void Main(string[] args)
        {
            // For now just process the hard coded input file.
            string[] lines = File.ReadAllLines("input.txt");

            // Loop over all lines in the input file and incoperate all the extracted values.
            int total = 0;
            foreach (string line in lines)
            {
                int firstDigit = extractFirstDigit(line);
                int secondDigit = extractSecondDigit(line);
                total += (firstDigit * 10) + secondDigit; // first digit is the 10's digit.
            }

            // Print the total to the screen before we exit.
            System.Console.WriteLine(total);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static int extractFirstDigit(string line)
        {
            int firstVal = -1;
            int firstIdx = int.MaxValue;
            
            for (int digitIdx = 1; digitIdx < digits.Length; digitIdx++)
            {
                int strIdx = line.IndexOf(digits[digitIdx]);
                if (strIdx != -1 && strIdx < firstIdx)
                {
                    firstIdx = strIdx;
                    firstVal = digitIdx / 2;
                }
            }

            return firstVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static int extractSecondDigit(string line)
        {
            int lastVal = -1;
            int lastIdx = int.MinValue;

            for (int digitIdx = 1; digitIdx < digits.Length; digitIdx++)
            {
                int strIdx = line.LastIndexOf(digits[digitIdx]);
                if (strIdx != -1 && strIdx > lastIdx)
                {
                    lastIdx = strIdx;
                    lastVal = digitIdx / 2;
                }
            }

            return lastVal;
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly string[] digits = {
            " ", "   ",
            "1", "one",
            "2", "two",
            "3", "three",
            "4", "four",
            "5", "five",
            "6", "six",
            "7", "seven",
            "8", "eight",
            "9", "nine"
        };

    }

}
