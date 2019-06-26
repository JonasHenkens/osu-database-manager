using osu_database_processor.Components;
using osu_database_processor.Enum;
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

        public bool AddCollection(Collection collection)
        {
            return AddCollection(collection, AddMode.Skip);
        }

        public bool AddCollection(Collection collection, AddMode addMode)
        {
            if (IsNameUsed(collection.Name))
            {
                switch (addMode)
                {
                    case AddMode.Skip:
                        return false;
                    case AddMode.Merge:
                        GetCollectionByName(collection.Name).MergeCollection(collection);
                        return true;
                    case AddMode.Overwrite:
                        RemoveCollection(collection.Name);
                        Collections.Add(collection);
                        return true;
                    default:
                        return false;
                }
            }
            Collections.Add(collection);
            return true;
        }

        public bool RemoveCollection(Collection collection)
        {
            return Collections.Remove(collection);
        }

        public bool RemoveCollection(string name)
        {
            return RemoveCollection(GetCollectionByName(name));
        }

        public bool IsNameUsed(string name)
        {
            foreach (Collection item in Collections)
            {
                if (item.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public Collection GetCollectionByName(string name)
        {
            foreach (Collection item in Collections)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
