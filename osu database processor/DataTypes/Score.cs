using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.DataTypes
{
    class Score
    {
        public byte Mode { get; private set; } // 0x00 = osu!Standard, 0x01 = Taiko, 0x02 = CTB, 0x03 = Mania
        public int Version { get; private set; } // osu version for this specific score
        public string BeatmapMD5 { get; private set; } // MD5 of beatmap
        public string PlayerName { get; private set; }
        public string ReplayMD5 { get; private set; }
        public short NumberOfHitValue1 { get; private set; } // Number of 300's
        public short NumberOfHitValue2 { get; private set; } // Number of 100's in osu!Standard, 150's in Taiko, 100's in CTB, 200's in Mania
        public short NumberOfHitValue3 { get; private set; } // Number of 50's in osu!Standard, small fruit in CTB, 50's in Mania
        public short NumberOfHitValue4 { get; private set; } // Number of Gekis in osu!Standard, Max 300's in Mania
        public short NumberOfHitValue5 { get; private set; } // Number of Katus in osu!Standard, 100's in Mania
        public short NumberOfMisses { get; private set; }
        public int ReplayScore { get; private set; }
        public float MaxCombo { get; private set; }
        public bool PerfectCombo { get; private set; }
        public int Mods { get; private set; } // Bitwise combination of mods used. See Osr (file format) for more information.
        // string: should always be empty
        public long Timestamp { get; private set; } // in windows ticks
        // int Constant, should always be 0xffffffff (-1)
        public long OnlineScoreID { get; private set; } // Online ScoresdbBeatmapScore ID

        public Score() { }

        public Score(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Mode = o.ReadByte();
            Version = o.ReadInt32();
            BeatmapMD5 = o.ReadString();
            PlayerName = o.ReadString();
            ReplayMD5 = o.ReadString();
            NumberOfHitValue1 = o.ReadInt16();
            NumberOfHitValue2 = o.ReadInt16();
            NumberOfHitValue3 = o.ReadInt16();
            NumberOfHitValue4 = o.ReadInt16();
            NumberOfHitValue5 = o.ReadInt16();
            NumberOfMisses = o.ReadInt16();
            ReplayScore = o.ReadInt32();
            MaxCombo = o.ReadInt16();
            PerfectCombo = o.ReadBoolean();
            Mods = o.ReadInt32();
            // string: should always be empty
            o.AssertString("", "Score: String isn't empty");
            Timestamp = o.ReadInt64();
            // int Constant, should always be 0xffffffff (-1)
            o.AssertInt(-1, "Score: int is not -1");
            OnlineScoreID = o.ReadInt64();
        }
    }
}
