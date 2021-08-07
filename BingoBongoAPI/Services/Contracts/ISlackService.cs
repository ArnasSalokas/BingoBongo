using BingoBongoAPI.Entities;
using BingoBongoAPI.Models.Response;
using System;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services.Contracts
{
    public interface ISlackService
    {
        public SlackCreateChannelResponse CreateChannel(Event newEvent);
        public void JoinEvent(string channel, string slackUserId);
        void PostToMediapark(string name);
    }
}
