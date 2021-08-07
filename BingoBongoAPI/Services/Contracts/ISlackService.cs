using BingoBongoAPI.Entities;
using System;
using System.Threading.Tasks;

namespace BingoBongoAPI.Services.Contracts
{
    public interface ISlackService
    {
        public Task CreateEvent(Event newEvent);
    }
}
