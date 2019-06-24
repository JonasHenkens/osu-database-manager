using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_api_wrapper.Components
{
    class Event
    {
        [JsonProperty("display_html")]
        public string DisplayHtml { get; set; }

        [JsonProperty("beatmap_id")]
        public int BeatmapId { get; set; }

        [JsonProperty("beatmapset_id")]
        public int BeatmapsetId { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("epicfactor")]
        public int Epicfactor { get; set; }
    }
}
