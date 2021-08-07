using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Request;
using BingoBongoAPI.Models.Response;
using BingoBongoAPI.Services.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services
{
    public class SlackService : ISlackService
    {
        private readonly RestClient _client;
        private const string token = "xoxb-2380245592192-2362846304372-GuouwhvV9SiokSKDY7RyFhFd";

        public SlackService()
        {
            _client = new RestClient("https://slack.com/api/");
        }

        public SlackCreateChannelResponse CreateChannel(Event newEvent)
        {
            var request = new RestRequest("conversations.create");
            request.AddParameter("name", newEvent.Name.ToLower());
            request.AddHeader("Authorization", $"Bearer {token}");
            var slackResponse = _client.Post(request);
            var json = slackResponse.Content; // Raw content as string

            var jObject = JObject.Parse(json);

            var response = new SlackCreateChannelResponse
            {
                Channel = new SlackChannel
                {
                    Id = jObject["channel"]["id"].ToString(),
                }
            };

            return response;
        }

        public void JoinEvent(string channel, string slackUserId)
        {
            var request = new RestRequest("conversations.invite");
            request.AddParameter("channel", channel);
            request.AddParameter("users", slackUserId);
            request.AddHeader("Authorization", $"Bearer {token}");
            var slackResponse = _client.Post(request);
            //var json = slackResponse.Content; // Raw content as string
        }

        public void PostToMediapark(string name)
        {
            var channel = "C0CVC9DBR";
            var token2 = "xoxb-13002103810-2370179269985-NmFyTY10z2RFWNs8miZXHOi6";
            var request = new RestRequest("chat.postMessage");
            request.AddParameter("channel", channel);
            request.AddParameter("text", $"<!here> A new amazing BingoBongo \"{name}\" event has been created! Want to be a part of it? Download the App!");
            request.AddHeader("Authorization", $"Bearer {token2}");
            var slackResponse = _client.Post(request);
        }
    }
}
