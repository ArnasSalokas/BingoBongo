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
        private const string token = "xoxb-2380245592192-2362846304372-oiOJXq7mv2ZUSTIfnOu0ZJtq";

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
    }
}
