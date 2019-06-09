using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.DataTypes
{
    class CollectionDb
    {
        public int Version { get; private set; }
        public int NumberOfCollections { get; private set; }
        public List<Collection> Collections { get; private set; }

        public CollectionDb() { }

        public CollectionDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Version = o.ReadInt32();
            NumberOfCollections = o.ReadInt32();
            Collections = new List<Collection>();
            for (int i = 0; i < NumberOfCollections; i++)
            {
                Collections.Add(new Collection(o));
            }
        }
    }
}
