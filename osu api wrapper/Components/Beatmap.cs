using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_api_wrapper.Components
{
    public class Beatmap
    {
        [JsonProperty("approved")]
        public RankedStatusAPI Approved { get; set; }

        [JsonProperty("submit_date")]
        public DateTime SubmitDate { get; set; }

        [JsonProperty("approved_date")]
        public DateTime ApprovedDate { get; set; }

        [JsonProperty("last_update")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("beatmap_id")]
        public int BeatmapId { get; set; }

        [JsonProperty("beatmapset_id")]
        public int BeatmapsetId { get; set; }

        [JsonProperty("bpm")]
        public double Bpm { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }

        [JsonProperty("difficultyrating")]
        public double Difficultyrating { get; set; }

        [JsonProperty("diff_aim")]
        public double DiffAim { get; set; }

        [JsonProperty("diff_speed")]
        public double DiffSpeed { get; set; }

        [JsonProperty("diff_size")]
        public float DiffSize { get; set; }

        [JsonProperty("diff_overall")]
        public float DiffOverall { get; set; }

        [JsonProperty("diff_approach")]
        public float DiffApproach { get; set; }

        [JsonProperty("diff_drain")]
        public float DiffDrain { get; set; }

        [JsonProperty("hit_length")]
        public int HitLength { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
        
        [JsonProperty("genre_id")]
        public Genre GenreId { get; set; }
        
        [JsonProperty("language_id")]
        public Language LanguageId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("total_length")]
        public int TotalLength { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("file_md5")]
        public string FileMd5 { get; set; }

        [JsonProperty("mode")]
        public Mode Mode { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("favourite_count")]
        public int FavouriteCount { get; set; }

        [JsonProperty("rating")]
        public float Rating { get; set; }

        [JsonProperty("playcount")]
        public int Playcount { get; set; }

        [JsonProperty("passcount")]
        public int Passcount { get; set; }

        [JsonProperty("count_normal")]
        public short CountNormal { get; set; }

        [JsonProperty("count_slider")]
        public short CountSlider { get; set; }

        [JsonProperty("count_spinner")]
        public short CountSpinner { get; set; }

        [JsonProperty("max_combo")]
        public short MaxCombo { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("download_unavailable")]
        public bool DownloadUnavailable { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("audio_unavailable")]
        public bool AudioUnavailable { get; set; }
    }
}
