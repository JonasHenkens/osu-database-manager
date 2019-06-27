using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_api_wrapper.Components
{
    class Game
    {
        [JsonProperty("game_id")]
        public int GameId { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("beatmap_id")]
        public int BeatmapId { get; set; }

        [JsonProperty("play_mode")]
        public Mode PlayMode { get; set; }

        // TODO: what is this?
        [JsonProperty("match_type")]
        public int MatchType { get; set; }

        [JsonProperty("scoring_type")]
        public WinningCondition ScoringType { get; set; }

        [JsonProperty("team_type")]
        public TeamType TeamType { get; set; }

        [JsonProperty("mods")]
        public Mods Mods { get; set; }

        [JsonProperty("scores")]
        public List<ScoreInfo> Scores { get; set; }
    }
}
