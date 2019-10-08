using System;
using System.Collections.Generic;
using RestSharp.Deserializers;
using Template.Entities.Integration.RandomGenerator.Base;

namespace Template.Entities.Integration.RandomGenerator
{
    public class RandomGenerateIntegerResponse : BaseRandomOrgResponse
    {
        [DeserializeAs(Name = "result")]
        public RandomGenerateIntegerResponseResult Result { get; set; }
    }

    public class RandomGenerateIntegerResponseResult : BaseRandomOrgResponseResult
    {
        [DeserializeAs(Name = "random")]
        public RandomGenerateIntegerResponseRandomData Random { get; set; }
    }
    public class RandomGenerateIntegerResponseRandomData
    {
        [DeserializeAs(Name = "completionTime")]
        public DateTime CompletionTime { get; set; }

        [DeserializeAs(Name = "data")]
        public IEnumerable<int> Data { get; set; }
    }
}
