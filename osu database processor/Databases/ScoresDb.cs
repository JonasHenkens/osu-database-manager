using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Databases
{
    class ScoresDb
    {
        public int Version { get; set; }
        public int NumberOfBeatmaps { get { return Beatmaps.Count; } }
        private List<BeatmapScores> Beatmaps;

        public ScoresDb(int version)
        {
            Version = version;
            Beatmaps = new List<BeatmapScores>();
        }

        public ScoresDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Version = o.ReadInt32();
            int numberOfBeatmaps = o.ReadInt32();
            Beatmaps = new List<BeatmapScores>();
            for (int i = 0; i < numberOfBeatmaps; i++)
            {
                Beatmaps.Add(new BeatmapScores(o));
            }
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write(Version);
            o.Write(NumberOfBeatmaps);
            foreach (var beatmap in Beatmaps)
            {
                beatmap.WriteToStream(o);
            }
        }

        public IReadOnlyList<BeatmapScores> GetBeatmapScores()
        {
            return Beatmaps.AsReadOnly();
        }

        public void AddBeatmapScores(BeatmapScores beatmapScores)
        {
            Beatmaps.Add(beatmapScores);
            // TODO: check if beatmap with same MD5 already present
        }

        public bool RemoveBeatmapScores(BeatmapScores beatmapScores)
        {
            return Beatmaps.Remove(beatmapScores);
        }
        // TODO: get BeatmapScores by MD5
    }
}
