using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    public class Person
    {
        public int PlayerId { get; private set; }
        public string Name { get; private set; }
        public byte Country1 { get; private set; }
        public byte Country2 { get; private set; }
        public byte Unknown { get; private set; } //TODO: unknown byte: most 0, regulary 4 and 96,  all found: 0 4 6 22 32 36 38 64 68 70 96 100 102
        public long Unknown2 { get; private set; } // TODO: 8 unknown bytes
        public int Rank { get; private set; }
        public long Unknown3 { get; private set; } // TODO: 8 unknown bytes

        public Person(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            PlayerId = o.ReadInt32();
            Name = o.ReadString();
            Country1 = o.ReadByte();
            Country2 = o.ReadByte();
            Unknown = o.ReadByte();
            Unknown2 = o.ReadInt64();
            Rank = o.ReadInt32();
            Unknown3 = o.ReadInt64();
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write(PlayerId);
            o.Write(Name);
            o.Write(Country1);
            o.Write(Country2);
            o.Write(Unknown);
            o.Write(Unknown2);
            o.Write(Rank);
            o.Write(Unknown3);
        }
    }
}
