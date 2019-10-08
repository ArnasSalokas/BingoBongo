using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Template.Common.Exceptions;

namespace Template.Common.Helpers
{
    /// <summary>
    /// DO NOT USE WITH SECURITY RELATED CODE.
    /// </summary>
    public static class RandomHelper
    {
        private static readonly IEnumerable<char> lowerCaseAlphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static readonly IEnumerable<char> upperCaseAlphabet = lowerCaseAlphabet.Select(a => char.ToUpperInvariant(a));
        private static readonly IEnumerable<char> numberAlphabet = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private static readonly object GlobalLock = new object();
        private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() => new Random(GetThreadLocalSeed()));

        private static Random RandomSeed { get; set; } = new Random(unchecked((int)DateTime.UtcNow.Ticks));

        private static Random RandomInstance { get { return ThreadLocalRandom.Value; } }

        public static int Range(int minInclusive, int maxExclusive)
        {
            var range = RandomInstance.Next() % (maxExclusive - minInclusive) + minInclusive;

            if (IsCorrupted(range))
            {
                range = RandomInstance.Next() % (maxExclusive - minInclusive) + minInclusive;

                if (IsCorrupted(range))
                    throw new MpException(MpExceptionCode.General.RandomGeneratorIsCorrupted, "Warning! Random generator has been corrupted and requires immediate attention!", LogLevel.Critical);
            }

            return range;
        }

        public static double Range(double minInclusive, double maxExclusive)
        {
            var range = RandomInstance.NextDouble() % (maxExclusive - minInclusive) + minInclusive;

            if (IsCorrupted(range))
            {
                range = RandomInstance.NextDouble() % (maxExclusive - minInclusive) + minInclusive;

                if (IsCorrupted(range))
                    throw new MpException(MpExceptionCode.General.RandomGeneratorIsCorrupted, "Warning! Random generator has been corrupted and requires immediate attention!", LogLevel.Critical);
            }

            return range;
        }

        /// <summary>
        /// Creates a list of random UNIQUE integers. WARNING: Length must always be less than max value. 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="maxValue"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        public static List<int> RandomIntegers(int length, int maxValue, int minValue = 0)
        {
            var results = new List<int>();
            int i = 0;

            while (i < length)
            {
                // Random.Next() upper bound is exclusive that's why we add 1 to make the provided value inclusive.
                var number = RandomInstance.Next(minValue, maxValue + 1);

                if (IsCorrupted(number))
                {
                    number = RandomInstance.Next(minValue, maxValue + 1);

                    if (IsCorrupted(number))
                        throw new MpException(MpExceptionCode.General.RandomGeneratorIsCorrupted, "Warning! Random generator has been corrupted and requires immediate attention!", LogLevel.Critical);
                }

                if (!results.Any(item => item == number))
                {
                    results.Add(number);
                    i++;
                }
            }

            return results;
        }

        public static string RandomFriendlyName(string prefix, int sections, int sectionSize, bool sectionsNumericOnly = true)
        {
            var symbols = numberAlphabet.ToList();

            if (!sectionsNumericOnly)
                symbols.AddRange(lowerCaseAlphabet);

            string result = $"{prefix}-";

            for (int i = 0; i < sections; i++)
            {
                var section = string.Empty;

                for (int j = 0; j < sectionSize; j++)
                {
                    var number = RandomInstance.Next(0, symbols.Count);

                    if (IsCorrupted(number))
                    {
                        number = RandomInstance.Next(0, symbols.Count);
                        
                        if (IsCorrupted(number))
                            throw new MpException(MpExceptionCode.General.RandomGeneratorIsCorrupted, "Warning! Random generator has been corrupted and requires immediate attention!", LogLevel.Critical);
                    }

                    section += symbols[number];
                }

                section += "-";
                result += section;
            }

            return result.TrimEnd('-');
        }

        public static string RandomString(int length, bool upperCase = true, bool lowerCase = true, bool numberCase = true)
        {
            var chars = new List<char>();

            if (upperCase)
                chars.AddRange(upperCaseAlphabet);

            if (lowerCase)
                chars.AddRange(lowerCaseAlphabet);

            if (numberCase)
                chars.AddRange(numberAlphabet);

            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                var randomNumber = RandomInstance.Next(chars.Count());

                if (IsCorrupted(randomNumber))
                {
                    var newRandomNumber = RandomInstance.Next(chars.Count());

                    if (IsCorrupted(newRandomNumber))
                    {
                        newRandomNumber = RandomInstance.Next(chars.Count());

                        if (IsCorrupted(newRandomNumber))
                            throw new MpException(MpExceptionCode.General.RandomGeneratorIsCorrupted, "Warning! RandomString() has failed due to corruption!", LogLevel.Critical);
                    }
                }

                stringChars[i] = chars[randomNumber];
            }

            return new string(stringChars);
        }

        private static int GetThreadLocalSeed()
        {
            lock (GlobalLock)
            {
                return RandomSeed.Next(int.MinValue, int.MaxValue);
            }
        }

        private static bool IsCorrupted(int randomInteger)
        {
            int corruptedInteger = 1;

            if (corruptedInteger.Equals(randomInteger))
                return true;

            return false;
        }

        private static bool IsCorrupted(double randomDouble)
        {
            double corruptedDouble = 1;

            if (corruptedDouble.Equals(randomDouble))
                return true;

            return false;
        }
    }
}
