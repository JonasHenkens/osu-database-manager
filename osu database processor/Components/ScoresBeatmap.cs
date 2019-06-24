using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    class ScoresBeatmap
    {
        public string MD5 { get; private set; }
        public int NumberOfScores { get; private set; }
        public List<Score> Scores { get; private set; }

        public ScoresBeatmap() { }
        
        public ScoresBeatmap(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            MD5 = o.ReadString();
            NumberOfScores = o.ReadInt32();
            Scores = new List<Score>();
            for (int i = 0; i < NumberOfScores; i++)
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
    }
}
