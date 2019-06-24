using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Databases
{
    class PresenceDb
    {
        public int Version { get; private set; }
        public int Amount { get; private set; }
        public List<Person> People { get; private set; }

        public PresenceDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Version = o.ReadInt32();
            Amount = o.ReadInt32();
            People = new List<Person>();
            for (int i = 0; i < Amount; i++)
            {
                People.Add(new Person(o));
            }
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write(Version);
            o.Write(Amount);
            foreach (var person in People)
            {
                person.WriteToStream(o);
            }
        }
    }
}
