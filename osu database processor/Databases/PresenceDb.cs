using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Databases
{
    class PresenceDb
    {
        public int Version { get; set; }
        public int Amount { get { return People.Count; } }
        private List<Person> People;

        public PresenceDb(int version)
        {
            Version = version;
            People = new List<Person>();
        }

        public PresenceDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Version = o.ReadInt32();
            int amount = o.ReadInt32();
            People = new List<Person>();
            for (int i = 0; i < amount; i++)
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

        public IReadOnlyList<Person> GetPeople()
        {
            return People.AsReadOnly();
        }

        public void AddPerson(Person person)
        {
            People.Add(person);
            // TODO: check if person with same id (or name?) already exists
        }

        public bool RemovePerson(Person person)
        {
            return People.Remove(person);
        }

        // TODO: get Person by name/id

    }
}
