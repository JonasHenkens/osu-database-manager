using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Databases
{
    class OsuDb
    {
        public int Version { get; private set; }
        public int FolderCount { get; private set; }
        public bool AccountUnlocked { get; private set; } // false when account is locked or banned
        public DateTime UnlockDate { get; private set; } // date account will be unlocked
        public String PlayerName { get; private set; }
        public int NumberOfBeatmaps { get; private set; }
        public List<Beatmap> Beatmaps { get; private set; } // beatmaps
        // Unknown, always seems to be 4

        public OsuDb() { }

        public OsuDb(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Version = o.ReadInt32();
            FolderCount = o.ReadInt32();
            AccountUnlocked = o.ReadBoolean();
            UnlockDate = o.ReadDateTime();
            PlayerName = o.ReadString();
            NumberOfBeatmaps = o.ReadInt32();
            Beatmaps = new List<Beatmap>();
            for (int i = 0; i < NumberOfBeatmaps; i++)
            {
                Beatmaps.Add(new Beatmap(o, Version));
            }
            o.AssertInt(4, "OsuDb: Unknown is not 4");
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write(Version);
            o.Write(FolderCount);
            o.Write(AccountUnlocked);
            o.Write(UnlockDate);
            o.Write(PlayerName);
            o.Write(NumberOfBeatmaps);
            foreach (var beatmap in Beatmaps)
            {
                beatmap.WriteToStream(o);
            }
            o.Write(4);
        }
    }
}
