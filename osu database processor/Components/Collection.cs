using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    public class Collection
    {
        public string Name { get; set; }
        public int NumberOfBeatmaps { get { return MD5s.Count; } }
        private List<string> MD5s;

        public Collection(string name)
        {
            Name = name;
            MD5s = new List<string>();
        }

        public Collection(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Name = o.ReadString();
            int numberOfBeatmaps = o.ReadInt32();
            MD5s = new List<string>();
            for (int i = 0; i < numberOfBeatmaps; i++)
            {
                MD5s.Add(o.ReadString());
            }
        }

        public void WriteToSteam(OsuWriter o)
        {
            o.Write(Name);
            o.Write(NumberOfBeatmaps);
            foreach (string item in MD5s)
            {
                o.Write(item);
            }
        }

        public IReadOnlyList<string> getMD5s()
        {
            return MD5s.AsReadOnly();
        }

        public void AddBeatmap(string md5)
        {
            if (!MD5s.Contains(md5))
            {
                MD5s.Add(md5);
            }
        }

        public bool RemoveBeatmap(string md5)
        {
            return MD5s.Remove(md5);
        }

        public void MergeCollection(Collection collection)
        {
            foreach (string item in collection.getMD5s())
            {
                AddBeatmap(item);
            }
        }
    }
}
