using Newtonsoft.Json;

namespace Template.Entities.Integration.RandomGenerator.Base
{
    public class BaseRandomOrgRequest
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get { return "2.0"; } } 

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
