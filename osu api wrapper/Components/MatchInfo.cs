using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_api_wrapper.Components
{
    class MatchInfo
    {
        [JsonProperty("match")]
        public Match Match { get; set; }

        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }
}
