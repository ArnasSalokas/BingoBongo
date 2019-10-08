using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using Template.Entities.Integration.RandomGenerator;
using Template.Entities.Integration.RandomGenerator.Base;
using Template.Services.Base;
using Template.Services.Services.Contracts;

namespace Template.Services.Services
{
    public class RandomGeneratorService : BaseRestService, IRandomGeneratorService
    {
        private const string GENERATE_INTEGERS_METHOD = "generateIntegers";

        public RandomGeneratorService(IServiceProvider services) : base(services)
        {
            BaseUrl = _config.RandomOrg.BaseUrl;
            ApiKey = _config.RandomOrg.ApiKey;
        }

        private static readonly IEnumerable<char> lowerCaseAlphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static readonly IEnumerable<char> upperCaseAlphabet = lowerCaseAlphabet.Select(a => char.ToUpperInvariant(a));
        private static readonly IEnumerable<char> numberAlphabet = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private static readonly object GlobalLock = new object();
        private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() => new Random(GetThreadLocalSeed()));

        private static Random RandomSeed { get; set; } = new Random(unchecked((int)DateTime.UtcNow.Ticks));

        private static Random RandomInstance { get { return ThreadLocalRandom.Value; } }

        public async Task<int> Range(int minInclusive, int maxExclusive)
        {
            var range = RandomInstance.Next() % (maxExclusive - minInclusive) + minInclusive;

            if (IsCorrupted(range))
                return await GetFallbackInteger(maxExclusive, minInclusive);

            else return range;
        }

        public async Task<double> Range(double minInclusive, double maxExclusive)
        {
            var range = RandomInstance.NextDouble() % (maxExclusive - minInclusive) + minInclusive;

            if (IsCorrupted(range))
                return await GetFallbackDouble(maxExclusive, minInclusive);

            else return range;
        }

        /// <summary>
        /// Creates a list of random UNIQUE integers. WARNING: Length must always be less than max value. 
        /// </summary>
        /// <param name="length"></param>
        /// <param name="maxValue"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        public async Task<List<int>> RandomIntegers(int length, int maxValue, int minValue = 0)
        {
            var results = new List<int>();
            int i = 0;

            while (i < length)
            {
                // Random.Next() upper bound is exclusive that's why we add 1 to make the provided value inclusive.
                var number = RandomInstance.Next(minValue, maxValue + 1);

                if (IsCorrupted(number))
                    number = await GetFallbackInteger(maxValue + 1, minValue);

                if (!results.Any(item => item == number))
                {
                    results.Add(number);
                    i++;
                }
            }

            return results;
        }

        public async Task<string> RandomString(int length, bool upperCase = true, bool lowerCase = true, bool numberCase = true)
        {
            var chars = new List<char>();
            var isCorrupted = false;

            if (upperCase) chars.AddRange(upperCaseAlphabet);

            if (lowerCase) chars.AddRange(lowerCaseAlphabet);

            if (numberCase) chars.AddRange(numberAlphabet);

            var stringChars = new char[length];

            for (int i = 0; i < stringChars.Length; i++)
            {
                var randomNumber = RandomInstance.Next(chars.Count());

                if (IsCorrupted(randomNumber))
                {
                    isCorrupted = true;
                    break;
                }

                stringChars[i] = chars[randomNumber];
            }

            if (isCorrupted)
            {
                var integers = await GetFallbackIntegers(chars.Count(), 0, stringChars.Length);

                for (int i = 0; i < integers.Count(); i++)
                {
                    stringChars[i] = chars[integers.ElementAt(i)];
                }
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

        private bool IsCorrupted(int randomInteger)
        {
            int corruptedInteger = 1;

            if (corruptedInteger.Equals(randomInteger))
                return true;

            return false;
        }

        private bool IsCorrupted(double randomDouble)
        {
            double corruptedDouble = 1;

            if (corruptedDouble.Equals(randomDouble))
                return true;

            return false;
        }

        private async Task<IEnumerable<int>> GetFallbackIntegers(int maxValue, int minValue, int count) => await GenerateRandomIntegers(maxValue, minValue, count);

        private async Task<int> GetFallbackInteger(int maxValue, int minValue = 0)
        {
            var integers = await GenerateRandomIntegers(maxValue, minValue);
            return integers.FirstOrDefault();
        }

        private async Task<IEnumerable<int>> GenerateRandomIntegers(int maxValue, int minValue = 0, int count = 1)
        {
            var body = new RandomGenerateIntegerRequest()
            {
                Method = GENERATE_INTEGERS_METHOD,
                Params = new RandomGenerateIntegerParameters()
                {
                    Key = ApiKey,
                    MaxValue = maxValue,
                    MinValue = minValue,
                    NumberOfDigits = count,
                    Replacement = true
                }
            };
            var request = FormRequest(body);
            var response = await SubmitRequest<RandomGenerateIntegerResponse>(request);

            return response.Data.Result.Random.Data;
        }

        private async Task<double> GetFallbackDouble(double maxValue, double minValue) => await GetFallbackInteger((int)maxValue, (int)minValue); 

        private IRestRequest FormRequest(BaseRandomOrgRequest body)
        {
            var request = BaseRequest(BaseUrl, Method.POST);
            body = body ?? new BaseRandomOrgRequest();

            request.AddJsonBody(body);

            return request;
        }

        private async Task<IRestResponse<T>> SubmitRequest<T>(IRestRequest request) where T : BaseRandomOrgResponse
        {
            var response = await ExecuteTaskAsync<T>(request);

            return response;
        }
    }
}
