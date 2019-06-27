using Newtonsoft.Json;
using System;

namespace osu_api_wrapper.Components
{
    class ScoreInfo
    {
        // get_user_best, get_user_recent
        [JsonProperty("beatmap_id", NullValueHandling = NullValueHandling.Ignore)]
        public int BeatmapId { get; set; }

        [JsonProperty("score_id")]
        public long ScoreId { get; set; }

        [JsonProperty("score")]
        public int ScoreScore { get; set; }

        // get_scores
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
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

        // always null for get_match
        [JsonProperty("enabled_mods", NullValueHandling = NullValueHandling.Ignore)]
        public Mods EnabledMods { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("rank")]
        public ScoreRank Rank { get; set; }

        // get_scores, get_user_best
        [JsonProperty("pp", NullValueHandling = NullValueHandling.Ignore)]
        public float Pp { get; set; }

        // get_scores
        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("replay_available", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReplayAvailable { get; set; }

        // get_match
        [JsonProperty("slot", NullValueHandling = NullValueHandling.Ignore)]
        public string Slot { get; set; }

        // get_match
        [JsonProperty("team", NullValueHandling = NullValueHandling.Ignore)]
        public string Team { get; set; }

        // get_match
        [JsonProperty("pass", NullValueHandling = NullValueHandling.Ignore)]
        public string Pass { get; set; }
    }
}