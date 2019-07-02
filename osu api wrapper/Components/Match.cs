using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_api_wrapper.Components
{
    public class Match
    {
        [JsonProperty("match_id")]
        public int MatchId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndTime { get; set; }
    }
}
