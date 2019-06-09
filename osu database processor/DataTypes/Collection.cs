using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.DataTypes
{
    class Collection
    {
        public string Name { get; private set; }
        public int NumberOfBeatmaps { get; private set; }
        public string[] MD5s { get; private set; }

        public Collection() { }

        public Collection(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Name = o.ReadString();
            NumberOfBeatmaps = o.ReadInt32();
            MD5s = new string[NumberOfBeatmaps];
            for (int i = 0; i < NumberOfBeatmaps; i++)
            {
                try
                {
                    MD5s[i] = o.ReadString();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}
