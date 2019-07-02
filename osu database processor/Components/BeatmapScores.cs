using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    public class BeatmapScores
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

        public bool AddScore(Score score)
        {
            if (MD5 == score.BeatmapMD5 && !Scores.Contains(score))
            {
                Scores.Add(score);
                return true;
            }
            return false;
        }
        
        public bool RemoveScore(Score score)
        {
            return Scores.Remove(score);
        }

        public bool MergeBeatmapScores(BeatmapScores beatmapScores)
        {
            if (MD5 == beatmapScores.MD5)
            {
                foreach (Score item in beatmapScores.GetScores())
                {
                    AddScore(item);
                }
                return true;
            }
            return false;
        }

        public Score GetHighestScore()
        {
            Score topScore = Scores[0];
            foreach (Score score in Scores)
            {
                if (score.ReplayScore > topScore.ReplayScore)
                {
                    topScore = score;
                }
            }
            return topScore;
        }

    }
}
