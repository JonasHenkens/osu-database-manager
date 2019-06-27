using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    class Score
    {
        public Mode Mode { get; set; } // 0x00 = osu!Standard, 0x01 = Taiko, 0x02 = CTB, 0x03 = Mania
        public int Version { get; set; } // osu version for this specific score
        public string BeatmapMD5 { get; set; } // MD5 of beatmap
        public string PlayerName { get; set; }
        public string ReplayMD5 { get; set; }
        public short NumberOfHitValue1 { get; set; } // Number of 300's
        public short NumberOfHitValue2 { get; set; } // Number of 100's in osu!Standard, 150's in Taiko, 100's in CTB, 200's in Mania
        public short NumberOfHitValue3 { get; set; } // Number of 50's in osu!Standard, small fruit in CTB, 50's in Mania
        public short NumberOfHitValue4 { get; set; } // Number of Gekis in osu!Standard, Max 300's in Mania
        public short NumberOfHitValue5 { get; set; } // Number of Katus in osu!Standard, 100's in Mania
        public short NumberOfMisses { get; set; }
        public int ReplayScore { get; set; }
        public short MaxCombo { get; set; }
        public bool PerfectCombo { get; set; }
        public Mods Mods { get; set; }
        // string: should always be empty
        public long Timestamp { get; set; } // in windows ticks
        // int Constant, should always be 0xffffffff (-1)
        public long OnlineScoreID { get; set; } // Online ScoresdbBeatmapScore ID

        public Score(Mode mode, int version, string beatmapMD5, string playerName, string replayMD5, short numberOfHitValue1, short numberOfHitValue2, short numberOfHitValue3, short numberOfHitValue4, short numberOfHitValue5, short numberOfMisses, int replayScore, short maxCombo, bool perfectCombo, Mods mods, long timestamp, long onlineScoreID)
        {
            Mode = mode;
            Version = version;
            BeatmapMD5 = beatmapMD5;
            PlayerName = playerName;
            ReplayMD5 = replayMD5;
            NumberOfHitValue1 = numberOfHitValue1;
            NumberOfHitValue2 = numberOfHitValue2;
            NumberOfHitValue3 = numberOfHitValue3;
            NumberOfHitValue4 = numberOfHitValue4;
            NumberOfHitValue5 = numberOfHitValue5;
            NumberOfMisses = numberOfMisses;
            ReplayScore = replayScore;
            MaxCombo = maxCombo;
            PerfectCombo = perfectCombo;
            Mods = mods;
            Timestamp = timestamp;
            OnlineScoreID = onlineScoreID;
        }

        public Score(OsuReader o)
        {
            ReadFromStream(o);
        }

        public void ReadFromStream(OsuReader o)
        {
            Mode = (Mode)o.ReadByte();
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
            Mods = (Mods)o.ReadInt32();
            // string: should always be empty
            o.AssertString(null, "Score: String isn't empty");
            Timestamp = o.ReadInt64();
            // int Constant, should always be 0xffffffff (-1)
            o.AssertInt(-1, "Score: int is not -1");
            OnlineScoreID = o.ReadInt64();
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write((byte)Mode);
            o.Write(Version);
            o.Write(BeatmapMD5);
            o.Write(PlayerName);
            o.Write(ReplayMD5);
            o.Write(NumberOfHitValue1);
            o.Write(NumberOfHitValue2);
            o.Write(NumberOfHitValue3);
            o.Write(NumberOfHitValue4);
            o.Write(NumberOfHitValue5);
            o.Write(NumberOfMisses);
            o.Write(ReplayScore);
            o.Write(MaxCombo);
            o.Write(PerfectCombo);
            o.Write((int)Mods);
            // string: should always be empty
            o.Write((byte)0);
            o.Write(Timestamp);
            // int Constant, should always be 0xffffffff (-1)
            o.Write(-1);
            o.Write(OnlineScoreID);
        }
    }
}
