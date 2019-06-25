using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    class BeatmapScores
    {
        public string MD5 { get; set; }
        public int NumberOfScores { get { return Scores.Count; } }
        private List<Score> Scores;

        public BeatmapScores(string md5)
        {
            MD5 = md5;
            Scores = new List<Score>();
        }
        
        public BeatmapScores(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            MD5 = o.ReadString();
            int numberOfScores = o.ReadInt32();
            Scores = new List<Score>();
            for (int i = 0; i < numberOfScores; i++)
            {
                Scores.Add(new Score(o));
            }
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write(MD5);
            o.Write(NumberOfScores);
            foreach (var score in Scores)
            {
                score.WriteToStream(o);
            }
        }

        public IReadOnlyList<Score> GetScores()
        {
            return Scores.AsReadOnly();
        }

        public void AddScore(Score score)
        {
            Scores.Add(score);
            // TODO: check if MD5 is correct
        }
        
        public bool RemoveScore(Score score)
        {
            return Scores.Remove(score);
        }
    }
}
