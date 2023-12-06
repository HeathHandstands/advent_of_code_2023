using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day_02
{

   // internal class InputDataFragment;

    using IDF = InputDataFragment;

    internal class Program
    {
        static void Main(string[] args)
        {

            //only 12 red cubes, 13 green cubes, and 14 blue cubes
            int[] maxCubes = { 12, 13, 14 };

            // Read the whole file, swap in as needed: "sample.input", "challenge.input"


            //
            bool useSampleData = true;
            string filePath = useSampleData ? "sample.input" : "challenge.input";
            string[] rawLines = File.ReadAllLines(filePath);
            IDF[] idfLines = Utils.CreateInitialisedArrayOf<IDF>(rawLines);
            Game[] games = Utils.CreateInitialisedArrayOf<Game>(idfLines);

        }

    }


    /// <summary>
    /// 
    /// </summary>
    internal class DiceSet
    {
        public Int64 reds;
        public Int64 greens;
        public Int64 blues;

        public DiceSet(IDF rawSet)
        {
            Int64 num = rawSet.ConsumeIntBefore(" ");
            string color = rawSet.ConsumeStringBefore(",");

            // Being lazy! just going by the length of the words to work out which color.
            switch (color.Length)
            {
                case 3:
                    reds += num;
                    break;

                case 5:
                    greens += num;
                    break;

                case 4:
                    blues += num;
                    break;

                default:
                    throw new Exception("Unexpected color!");
            }
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class Game 
    {
        public Int64 id;
        public List<DiceSet> sets;

        public Game(IDF rawGame) 
        {
            while (rawGame.DataAvailable())
            {
                this.id = rawGame.ConsumeIntBefore(":");
                IDF rawSet = rawGame.ConsumeBefore(";");
                DiceSet diceSet = new DiceSet(rawSet);
                sets.Add(diceSet);
            }
        }
    }









    /// <summary>
    /// 
    /// </summary>
    internal class InputDataFragment
    {
        //
        int idx;

        //
        int idxMax;

        //
        string rawData;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static InputDataFragment[] ReadWholeFile(string filePath)
        {
            string[] inputLines = File.ReadAllLines(filePath);
            InputDataFragment[] fragments = new InputDataFragment[inputLines.Length];

            for (int i = 0; i < inputLines.Length; i++)
            {
                fragments[i] = new InputDataFragment(inputLines[i]);
            }

            return fragments;
        }

        /// <summary>
        /// 
        /// </summary>
        private InputDataFragment() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        public InputDataFragment(string rawData)
        {
            this.idx = 0;
            this.idxMax = rawData.Length;
            this.rawData = rawData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <param name="idxMax"></param>
        private InputDataFragment(InputDataFragment other, int idxMax)
        {
            this.idx = other.idx;
            this.idxMax = idxMax;
            this.rawData = other.rawData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CharactersRemaining()
        {
            return this.idxMax - this.idx; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DataAvailable()
        {
            return (CharactersRemaining() > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delim"></param>
        /// <param name="discardDelim"></param>
        /// <returns></returns>
        public InputDataFragment ConsumeBefore(string delim, bool discardDelim = true)
        {
            int delimLoc = rawData.IndexOf(delim, idx);
            int newIdx = delimLoc + (discardDelim ? delim.Length : 0);
            return new InputDataFragment(this, delimLoc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delim"></param>
        /// <param name="discardDelim"></param>
        /// <returns></returns>
        public string ConsumeStringBefore(string delim, bool discardDelim = true)
        {
            // Find the location of the next occurence of the delimiter (after
            // the current location). If it's not found use all remaning data.
            int delimLoc = rawData.IndexOf(delim, idx);
            int delimLength = delim.Length;
            if (delimLoc == -1)
            {
                delimLoc = idxMax;
                delimLength = 0;
            }

            // Extract the string we are going to return, update the index now
            // that we are done with it and return the string we have extracted.
            string retVal = rawData.Substring(idx, delimLoc - idx);
            idx = delimLoc + (discardDelim ? delimLength : 0);
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delim"></param>
        /// <param name="discardDelim"></param>
        /// <returns></returns>
        public Int64 ConsumeIntBefore(string delim, bool discardDelim = true)
        {
            // Find the location of the next occurence of the delimiter (after the current location).
            int delimLoc = rawData.IndexOf(delim, idx);

            // Calcualte the location that current will be updated to at the end of this opperation.
            int newIdx = delimLoc + (discardDelim ? delim.Length : 0);

            // Initiliase the varaibles we need for the loop below.
            Int64 retVal = 0;
            Int64 digitMultiplier = 1;
            int digitLoc = delimLoc - 1;

            // In reverce from just before the delimeter incperate each digit into the
            // return value (first the 1's then the 10's, 100's, etc).
            while (Char.IsDigit(rawData[digitLoc]))
            {
                retVal += AsciiDigitToIntVal(rawData[digitLoc]) * digitMultiplier;
                digitMultiplier = digitMultiplier * 10;
                digitLoc = digitLoc - 1;
            }

            // If there is a '-' right before the first digit make the retVal negative.
            if (rawData[digitLoc] != '-')
            {
                retVal = retVal * -1;
            }

            // Finaly!
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        private static Int64 AsciiDigitToIntVal(char digit)
        {
            return (Int64)(digit - 48);
        }

    }

}
