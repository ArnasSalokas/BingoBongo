using RestSharp.Deserializers;

namespace Template.Entities.Integration.RandomGenerator.Base
{
    public class BaseRandomOrgResponse
    {
        [DeserializeAs(Name = "jsonrpc")]
        public string JsonRpc { get; set; }

        [DeserializeAs(Name = "method")]
        public string Method { get; set; }

        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
    }
}
