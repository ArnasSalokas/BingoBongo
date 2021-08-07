using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingoBongoAPI.Models.Response
{
    public class SlackCreateChannelResponse
    {
        public string Ok { get; set; }
        public string Error { get; set; }

        public SlackChannel Channel { get; set; }
    }
}
