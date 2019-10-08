using Newtonsoft.Json;
using Template.Entities.Integration.RandomGenerator.Base;

namespace Template.Entities.Integration.RandomGenerator
{
    public class RandomGenerateIntegerRequest : BaseRandomOrgRequest
    { 
        [JsonProperty("params")]
        public RandomGenerateIntegerParameters Params { get; set; }
    }

    public class RandomGenerateIntegerParameters
    {
        [JsonProperty("apiKey")]
        public string Key { get; set; }

        [JsonProperty("n")]
        public int NumberOfDigits { get; set; }

        [JsonProperty("min")]
        public int MinValue { get; set; }

        [JsonProperty("max")]
        public int MaxValue { get; set; }

        [JsonProperty("replacement")]
        public bool Replacement { get; set; } // determines if number should be unique (false) or contain duplicates (true)
    }
}
