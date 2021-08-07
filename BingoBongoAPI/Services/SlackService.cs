using BingoBongoAPI.Entities;
using BingoBongoAPI.Services.Contracts;
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

        public SlackService()
        {
            _client = new RestClient("https://slack.com/api/");
        }

        public async Task CreateEvent(Event newEvent)
        {
            var request = new RestRequest("conversations.create");
            request.AddParameter("name", newEvent.Name);
            request.AddHeader("header", "value");
            var response = _client.Post(request);
            var content = response.Content; // Raw content as string

        }
    }
}
