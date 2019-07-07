using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace osu_database_processor.Databases
{
    public class ScoresDb
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

        public ScoresDb(string path)
        {
            OsuReader or = new OsuReader(new FileStream(path, FileMode.Open));
            ReadFromStream(or);
            or.Close();
        }

        public void ReadFromStream(OsuReader o)
        {
            try
            {

                Version = o.ReadInt32();
                int numberOfBeatmaps = o.ReadInt32();
                Beatmaps = new List<BeatmapScores>();
                for (int i = 0; i < numberOfBeatmaps; i++)
                {
                    Beatmaps.Add(new BeatmapScores(o));
                }
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(InvalidDataException)) throw;
                else throw new InvalidDataException("Invalid data", e);
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

        public bool AddBeatmapScores(BeatmapScores beatmapScores, AddMode addMode)
        {
            if (IsBeatmapScoresPresent(beatmapScores.MD5))
            {
                switch (addMode)
                {
                    case AddMode.Skip:
                        return false;
                    case AddMode.Merge:
                        GetBeatmapScoresByMD5(beatmapScores.MD5).MergeBeatmapScores(beatmapScores);
                        return true;
                    case AddMode.Overwrite:
                        RemoveBeatmapScores(GetBeatmapScoresByMD5(beatmapScores.MD5));
                        Beatmaps.Add(beatmapScores);
                        return true;
                    default:
                        return false;
                }
            }
            Beatmaps.Add(beatmapScores);
            return true;
        }

        public bool RemoveBeatmapScores(BeatmapScores beatmapScores)
        {
            return Beatmaps.Remove(beatmapScores);
        }

        public BeatmapScores GetBeatmapScoresByMD5(string md5)
        {
            foreach (BeatmapScores item in Beatmaps)
            {
                if (item.MD5 == md5)
                {
                    return item;
                }
            }
            return null;
        }

        public bool IsBeatmapScoresPresent(string md5)
        {
            foreach (BeatmapScores item in Beatmaps)
            {
                if (item.MD5 == md5)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
