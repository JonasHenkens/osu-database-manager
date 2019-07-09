using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace osu_database_processor.Databases
{
    public class CollectionDb
    {
        public int Version { get; set; }
        public int NumberOfCollections { get { return Collections.Count; } }
        private List<Collection> Collections;

        public CollectionDb(int version)
        {
            Version = version;
            Collections = new List<Collection>();
        }

        public CollectionDb(string path)
        {
            OsuReader or = new OsuReader(new FileStream(path, FileMode.Open));
            ReadFromStream(or);
            or.Close();
        }

        public CollectionDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            try
            {
                Version = o.ReadInt32();
                int numberOfCollections = o.ReadInt32();
                Collections = new List<Collection>();
                for (int i = 0; i < numberOfCollections; i++)
                {
                    Collections.Add(new Collection(o));
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
            o.Write(NumberOfCollections);
            foreach (Collection item in Collections)
            {
                item.WriteToSteam(o);
            }
        }

        public string WriteToFile(string path)
        {
            string newPath = path;
            int i = 2;
            while (File.Exists(newPath))
            {
                string suffix = " (" + i + ")";
                newPath = Path.Combine(Path.GetDirectoryName(path), string.Concat(Path.GetFileNameWithoutExtension(path), suffix, Path.GetExtension(path)));
                i++;
            }
            OsuWriter o = new OsuWriter(new FileStream(newPath, FileMode.CreateNew));
            WriteToStream(o);
            o.Flush();
            o.Close();
            return newPath;
        }

        public IReadOnlyList<Collection> GetCollections()
        {
            return Collections.AsReadOnly();
        }

        /// <summary>
        /// Skips collection if one with same name exists.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
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

        public void Merge(CollectionDb collectionDb, AddMode addMode)
        {
            foreach (Collection collection in collectionDb.Collections)
            {
                AddCollection(collection, addMode);
            }
        }
    }
}
