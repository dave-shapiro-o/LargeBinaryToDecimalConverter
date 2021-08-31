using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;
using static System.Console;
using static System.Numerics.BigInteger;
namespace BinaryConverter
{
    class LargeBinaryToDecimal
    {
        static BigInteger answer;
        static BigInteger positionalValue;

        static int length;
        static int subLength = 2048;
        static int remainder;
        static int ZerosToAppend;

        static string input;
        static bool isPadded;
        static Stopwatch clock;

        static void Main(string[] args)
        {
            ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                answer = 0;
                positionalValue = 1;
                isPadded = false;
                remainder = 0;
                ZerosToAppend = 0;
                clock = new Stopwatch();

                WriteLine("enter binary number, 'Q' to quit");
                SetIn(new StreamReader(OpenStandardInput(), Encoding.UTF8, false, (int)Math.Pow(2, 21)));
                input = ReadLine();
                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                    Environment.Exit(0);

                length = input.Length;
                if (length < 2 * subLength)
                {
                    CalculateSmallerValue();
                }
                else
                {
                    CalculateLargerValue();
                }
                if (isPadded)
                {
                    // Don't count the padded zeros as part of the original length
                    length -= ZerosToAppend;
                }
                clock.Stop();
                WriteLine($"\ncompleted conversion of {length} digit binary in {clock.Elapsed}");
                WriteLine("Converting to Exponential notation...");
                WriteLine($"Decimal conversion: {answer.ToString("E")}\n");
                WriteLine($"Number of atoms in the observable universe: 10E+82\n");
                WriteLine($"Full number: {answer}\n");
            }
        }
        private static void CalculateSmallerValue()
        {
            clock.Start();
            // add all input chars to int array
            int[] shortTempArray = new int[length];
            for (int i = 0; i < length; i++)
                shortTempArray[i] = input[i] - '0';

            // Multiply each by positional value and add to answer 
            for (int i = length - 1; i >= 0; i--)
            {
                answer += shortTempArray[i] * positionalValue;
                positionalValue = Multiply(positionalValue, 2);
            }
            WriteLine("\nCalculated with short algorithm:\n");
        }

        private static void CalculateLargerValue()
        {
            clock.Start();
            int[] tempArray = new int[subLength];
            BigInteger tempAnswer = new BigInteger();
            remainder = length % subLength;

            if (remainder != 0)
            {
                PadWithZeros();
            }
            // First sublength
            int subStart = length - subLength;
            int subEnd = subStart + subLength - 1;

            for (int i = 0; i < length / subLength; i++)
            {
                // Iterate through each sublength
                int subPosition = subLength - 1;
                for (int j = subEnd; j >= subStart; j--)
                {
                    // Fill temp array with values, multiply by positional values as if in rightmost sublength
                    tempArray[subPosition] = input[j] - '0';
                    tempAnswer += tempArray[subPosition--] * positionalValue;
                    positionalValue = Multiply(positionalValue, 2);
                }
                if (subEnd < length - 1)
                {
                    // If not rightmost sublength, multiply temp total by positional adjuster
                    BigInteger adjuster = Pow(2, length - subEnd - 1);
                    tempAnswer = Multiply(tempAnswer, adjuster);
                }
                // Add temp total to answer, reset variables and move on to next sublength
                answer += tempAnswer;
                tempAnswer = 0;
                subStart -= subLength;
                subEnd -= subLength;
                positionalValue = 1;
            }
            WriteLine("Calculated with \"Divide and Conquer\" algorithm:");
        }
        private static void PadWithZeros()
        {
            StringBuilder builder = new StringBuilder();
            ZerosToAppend = subLength - remainder;
            length += ZerosToAppend;
            for (int i = 0; i < ZerosToAppend; i++)
                builder.Append("0");
            builder.Append(input);
            input = builder.ToString();
            isPadded = true;
        }
    }
}


