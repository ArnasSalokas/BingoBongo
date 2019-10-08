using RestSharp.Deserializers;

namespace Template.Entities.Integration.RandomGenerator.Base
{
    public class BaseRandomOrgResponseResult
    {
        [DeserializeAs(Name = "bitsUsed")]
        public int BitsUsed { get; set; }

        [DeserializeAs(Name = "bitsLeft")]
        public int BitsLeft { get; set; }

        [DeserializeAs(Name = "requestsLeft")]
        public int RequestsLeft { get; set; }

        [DeserializeAs(Name = "advisoryDelay")]
        public int AdvisoryDelay { get; set; }
    }
}
