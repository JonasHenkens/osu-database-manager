using Newtonsoft.Json;
using osu_api_wrapper.Enum;
using System;
using static osu_api_wrapper.Components.Converters;

namespace osu_api_wrapper.Components
{
    class ScoreInfo
    {
        [JsonProperty("score_id")]
        public long ScoreId { get; set; }

        [JsonProperty("score")]
        public int ScoreScore { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("count300")]
        public short Count300 { get; set; }

        [JsonProperty("count100")]
        public short Count100 { get; set; }

        [JsonProperty("count50")]
        public short Count50 { get; set; }

        [JsonProperty("countmiss")]
        public short Countmiss { get; set; }

        [JsonProperty("maxcombo")]
        public short Maxcombo { get; set; }

        [JsonProperty("countkatu")]
        public short Countkatu { get; set; }

        [JsonProperty("countgeki")]
        public short Countgeki { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("perfect")]
        public bool Perfect { get; set; }

        [JsonProperty("enabled_mods")]
        public Mods EnabledMods { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("rank")]
        public ScoreRank Rank { get; set; }

        [JsonProperty("pp")]
        public float Pp { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("replay_available")]
        public bool ReplayAvailable { get; set; }

    }
}