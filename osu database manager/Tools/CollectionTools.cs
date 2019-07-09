using osu_database_processor.Components;
using osu_database_processor.Databases;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_manager.Tools
{
    class CollectionTools
    {
        /// <summary>
        /// Use scoresDb to generate collections based on accuracy.
        /// </summary>
        /// <param name="scoresDb"></param>
        /// <param name="seperators"></param>
        /// <param name="name"></param>
        /// <param name="collectionDb">Merge collections into existing collectionDb. Overwrites existing collections.</param>
        /// <returns></returns>
        public static CollectionDb GenerateCollectionDbByAccuracy(ScoresDb scoresDb, List<int> seperators, string name = null, string prefix = "", CollectionDb collectionDb = null)
        {
            List<Score> scores = new List<Score>();
            foreach (BeatmapScores beatmapScores in scoresDb.GetBeatmapScores())
            {
                Score topScore = null;

                if (string.IsNullOrWhiteSpace(name)) topScore = beatmapScores.GetHighestScore();
                else topScore = beatmapScores.GetHighestScore(name);

                if (topScore != null) scores.Add(topScore);
            }
            List<Collection> collections = GenerateCollectionsByAccuracy(scores, seperators, prefix);

            CollectionDb colDb = new CollectionDb(20190620);
            foreach (Collection collection in collections)
            {
                colDb.AddCollection(collection);
            }

            if (collectionDb != null)
            {
                collectionDb.Merge(colDb, osu_database_processor.AddMode.Overwrite);
                colDb = collectionDb;
            }

            return colDb;
        }

        public static List<Collection> GenerateCollectionsByAccuracy(List<Score> scores, List<int> seperators, string prefix = "")
        {
            List<Score> scoresC = new List<Score>(scores);
            seperators.Sort();
            List<Collection> collections = new List<Collection>();

            int min = 0;
            int max = 0;
            foreach (int seperator in seperators)
            {
                min = max;
                max = seperator;
                Collection collection = new Collection(prefix + min + " - <" + max);
                Console.WriteLine(prefix + min + " - <" + max);
                Console.WriteLine("seperator " + seperator);
                Console.WriteLine("min " + min);
                Console.WriteLine("max " + max);

                foreach (Score score in scores)
                {
                    if (score.GetAccuracy() >= min && score.GetAccuracy() < max)
                    {
                        collection.AddBeatmap(score.BeatmapMD5);
                        scoresC.Remove(score);
                    }
                }
                collections.Add(collection);
            }

            if (max <= 100)
            {
                Collection collection = new Collection(prefix + max + " - 100");
                if (max == 100) collection.Name = prefix + "100";
                foreach (Score score in scoresC)
                {
                    collection.AddBeatmap(score.BeatmapMD5);
                }
            }

            return collections;
        }

        public static Collection GenerateCollectionByAccuracyOnline()
        {


            throw new NotImplementedException();
        }
    }
}
