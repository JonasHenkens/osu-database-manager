using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Databases
{
    class CollectionDb
    {
        public int Version { get; set; }
        public int NumberOfCollections { get { return Collections.Count; } }
        private List<Collection> Collections;

        public CollectionDb(int version)
        {
            Version = version;
            Collections = new List<Collection>();
        }

        public CollectionDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Version = o.ReadInt32();
            int numberOfCollections = o.ReadInt32();
            Collections = new List<Collection>();
            for (int i = 0; i < numberOfCollections; i++)
            {
                Collections.Add(new Collection(o));
            }
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write(Version);
            o.Write(NumberOfCollections);
            foreach (Collection item in Collections)
            {
                item.WriteToSteam(o);
            }
        }

        public IReadOnlyList<Collection> GetCollections()
        {
            return Collections.AsReadOnly();
        }

        public void AddCollection(Collection collection)
        {
            Collections.Add(collection);
            // TODO: check if collection with same name exists
        }
        
        public bool RemoveCollection(Collection collection)
        {
            return Collections.Remove(collection);
        }

        // TODO: add method to check if collection with name already exists
    }
}
