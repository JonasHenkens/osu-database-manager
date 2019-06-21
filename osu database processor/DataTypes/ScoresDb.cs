using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.DataTypes
{
    class ScoresDb
    {
        public int Version { get; private set; }
        public int NumberOfBeatmaps { get; private set; }
        public List<ScoresBeatmap> Beatmaps { get; private set; }

        public ScoresDb() { }

        public ScoresDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Version = o.ReadInt32();
            NumberOfBeatmaps = o.ReadInt32();
            Beatmaps = new List<ScoresBeatmap>();
            for (int i = 0; i < NumberOfBeatmaps; i++)
            {
                Beatmaps.Add(new ScoresBeatmap(o));
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
    }
}
