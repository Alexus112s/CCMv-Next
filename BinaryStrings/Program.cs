using System;

namespace BinaryStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1100".IsGoodBinaryString());
            Console.WriteLine("1010".IsGoodBinaryString());
            Console.WriteLine("11001100".IsGoodBinaryString());
            Console.WriteLine("11110000".IsGoodBinaryString());

            Console.WriteLine("1l00".IsGoodBinaryString()); // with L :)
            Console.WriteLine("110000".IsGoodBinaryString());
            Console.WriteLine("1111".IsGoodBinaryString());
            Console.WriteLine("0011".IsGoodBinaryString());
            Console.WriteLine("10111".IsGoodBinaryString());
            Console.WriteLine("test".IsGoodBinaryString());
            Console.WriteLine(string.Empty.IsGoodBinaryString());
            Console.WriteLine(((string)null).IsGoodBinaryString());
        }
    }

    static class BinaryStringExtensions
    {
        /// <summary>
        /// Tests a string if it is a "Good binary string".
        /// <para>We consider a non-empty binary string to be good if the following two conditions are true:</para>
        /// <list type="number">
        /// <item>The number of 0's is equal to the number of 1's.</item>
        /// <item>For every prefix of the binary string, the number of 1's should not be less than the number of 0's.</item>
        /// </list>
        /// </summary>
        /// <param name="s">A string to test.</param>
        /// <returns><see cref="true"/>, if the input is a "Good binary string", otherwise <see cref="false"/>.</returns>
        public static bool IsGoodBinaryString(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return false; // TODO: consider throwing an ArgumentException/ArgumentNullException.

            if (s.Length % 2 == 1)
                return false; // optimized scenario.

            var zeroes = 0;
            var ones = 0;

            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case '0':
                        zeroes++;
                        break;
                    case '1':
                        ones++;
                        break;
                    default:
                        return false; // not a binary string! TODO: consider throwing an ArgumentException.
                }

                if (ones < zeroes)
                    return false; // does not satisfy the prefix condition.
            }

            return zeroes == ones;
        }
    }
}
