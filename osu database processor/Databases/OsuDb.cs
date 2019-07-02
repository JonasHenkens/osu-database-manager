﻿using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Databases
{
    public class OsuDb
    {
        public int Version { get; set; }
        public int FolderCount { get; set; } // actual amount of folders in songs directory, only used to detect changes, doesn't matter if wrong (osu gives warning and updates value)
        // TODO: implement editing of AccountUnlocked and UnlockDate
        public bool AccountUnlocked { get; private set; } // false when account is locked or banned
        public DateTime UnlockDate { get; private set; } // date account will be unlocked
        public String PlayerName { get; set; }
        public int NumberOfBeatmaps { get { return Beatmaps.Count; } }
        private List<Beatmap> Beatmaps;
        // Unknown, always seems to be 4

        public OsuDb(int version, string playername)
        {
            Version = version;
            FolderCount = 0;
            AccountUnlocked = true;
            UnlockDate = new DateTime();
            PlayerName = playername;
            Beatmaps = new List<Beatmap>();
        }

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
            int numberOfBeatmaps = o.ReadInt32();
            Beatmaps = new List<Beatmap>();
            for (int i = 0; i < numberOfBeatmaps; i++)
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

        public IReadOnlyList<Beatmap> GetBeatmaps()
        {
            return Beatmaps.AsReadOnly();
        }

        public bool AddBeatmap(Beatmap beatmap)
        {
            return AddBeatmap(beatmap, AddMode.Skip);
        }

        public bool AddBeatmap(Beatmap beatmap, AddMode addMode)
        {
            if (IsBeatmapPresent(beatmap.MD5Beatmap))
            {
                switch (addMode)
                {
                    case AddMode.Skip:
                        return false;
                    case AddMode.Merge:
                        return false;
                    case AddMode.Overwrite:
                        RemoveBeatmap(beatmap.MD5Beatmap);
                        break;
                    default:
                        return false;
                }
            }
            Beatmaps.Add(beatmap);
            return true;
            // TODO: FolderCount++ if new folder OR add UpdateFolderCount method (checks amount of folders)
        }

        public bool RemoveBeatmap(Beatmap beatmap)
        {
            return Beatmaps.Remove(beatmap);
            // TODO: FolderCount-- if all from folder removed OR add UpdateFolderCount method (checks amount of folders)
        }

        public bool RemoveBeatmap(string md5)
        {
            return RemoveBeatmap(GetBeatmapByMD5(md5));
        }

        public Beatmap GetBeatmapByMD5(string md5)
        {
            foreach (Beatmap item in Beatmaps)
            {
                if (item.MD5Beatmap == md5)
                {
                    return item;
                }
            }
            return null;
        }

        public bool IsBeatmapPresent(string md5)
        {
            foreach (Beatmap item in Beatmaps)
            {
                if (item.MD5Beatmap == md5)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
