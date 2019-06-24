using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    class Beatmap
    {

        public Beatmap() { }
        public int SizeInBytes { get; private set; } // size of this entry
        public string ArtistName { get; private set; }
        public string ArtistNameUnicode { get; private set; }
        public string SongTitle { get; private set; }
        public string SongTitleUnicode { get; private set; }
        public string CreatorName { get; private set; }
        public string Difficulty { get; private set; } // e.g. Hard, Insane, etc.
        public string AudioFileName { get; private set; }
        public string MD5Beatmap { get; private set; } // MD5 of beatmap
        public string NameDotOsuFile { get; private set; } // name of .osu file
        public byte RankedStatus { get; private set; } // 0 = unknown, 1 = unsubmitted, 2 = pending/wip/graveyard, 3 = unused, 4 = ranked, 5 = approved, 6 = qualified, 7 = loved
        public short NumberOfHitcircles { get; private set; }
        public short NumberOfSliders { get; private set; } // note: will be present in every mode
        public short NumberOfSpinners { get; private set; } // note: will be present in every mode
        public long ModificationTime { get; private set; } // last modification time in windows ticks

        // next 4 are bytes for versions smaller than 20140609
        public float ApproacRate { get; private set; }
        public float CircleSize { get; private set; }
        public float HPDrain { get; private set; }
        public float OverallDifficulty { get; private set; }
        // for version smaller than 20140609
        public byte ApproacRateB { get; private set; }
        public byte CircleSizeB { get; private set; }
        public byte HPDrainB { get; private set; }
        public byte OverallDifficultyB { get; private set; }
        //

        public double SliderVelocity { get; private set; }

        // next part only present if version greater or equal to 20140609
        //  IntDoublePair: int is mod combination, double is star rating
        public int AmountOfPairsStandard { get; private set; }
        public List<IntDoublePair> PairsStandard { get; private set; }
        public int AmountOfPairsTaiko { get; private set; }
        public List<IntDoublePair> PairsTaiko { get; private set; }
        public int AmountOfPairsCTB { get; private set; }
        public List<IntDoublePair> PairsCTB { get; private set; }
        public int AmountOfPairsMania { get; private set; }
        public List<IntDoublePair> PairsMania { get; private set; }
        //

        public int DrainTime { get; private set; } // in seconds
        public int TotalTime { get; private set; } // in miliseconds
        public int TimeOfPreview { get; private set; } // in miliseconds, starting time of audiopreview
        public int AmountOfTimingPoints { get; private set; }
        public List<Timingpoint> TimingPoints { get; private set; }
        public int BeatmapID { get; private set; }
        public int BeatmapSetID { get; private set; }
        public int ThreadID { get; private set; }
        public byte GradeAchievedStandard { get; private set; }
        public byte GradeAchievedTaiko { get; private set; }
        public byte GradeAchievedCTB { get; private set; }
        public byte GradeAchievedMania { get; private set; }
        public short LocalBeatmapOffset { get; private set; }
        public float StackLeniency { get; private set; }
        public byte GameplayMode { get; private set; } // Osu gameplay mode. 0x00 = osu!Standard, 0x01 = Taiko, 0x02 = CTB, 0x03 = Mania
        public string SongSource { get; private set; }
        public string SongTags { get; private set; }
        public short OnlineOffset { get; private set; }
        public string Font { get; private set; }
        public bool Unplayed { get; private set; }
        public long LastTimePlayed { get; private set; }
        public bool Osz2 { get; private set; } // is the beatmap osz2
        public string FolderName { get; private set; } // relative to songs folder
        public long LastTimeChecked { get; private set; } // last time the beatmap was checked against osu! repository
        public bool IgnoreBeatmapSound { get; private set; }
        public bool IgnoreBeatmapSkin { get; private set; }
        public bool DisableStoryboard { get; private set; }
        public bool DisableVideo { get; private set; }
        public bool VisualOverride { get; private set; }

        public short Unknown { get; private set; } // only present if version less than 20140609

        public int LastModificationTime { get; private set; } // ?
        public byte ManiaScrollSpeed { get; private set; }

        public int Version { get; private set; }

        public Beatmap(OsuReader o, int version)
        {
            ReadFromStream(o, version);
        }

        public void ReadFromStream(OsuReader o, int version)
        {
            Version = version;

            SizeInBytes = o.ReadInt32();
            ArtistName = o.ReadString();
            ArtistNameUnicode = o.ReadString();
            SongTitle = o.ReadString();
            SongTitleUnicode = o.ReadString();
            CreatorName = o.ReadString();
            Difficulty = o.ReadString();
            AudioFileName = o.ReadString();
            MD5Beatmap = o.ReadString();
            NameDotOsuFile = o.ReadString();
            RankedStatus = o.ReadByte();
            NumberOfHitcircles = o.ReadInt16();
            NumberOfSliders = o.ReadInt16();
            NumberOfSpinners = o.ReadInt16();
            ModificationTime = o.ReadInt64();

            // next 4 are bytes for versions smaller than 20140609
            if (version >= 20140609)
            {
                ApproacRate = o.ReadSingle();
                CircleSize = o.ReadSingle();
                HPDrain = o.ReadSingle();
                OverallDifficulty = o.ReadSingle();
            }
            else
            {
                ApproacRateB = o.ReadByte();
                CircleSizeB = o.ReadByte();
                HPDrainB = o.ReadByte();
                OverallDifficultyB = o.ReadByte();
            }

            SliderVelocity = o.ReadDouble();

            // next part only present if version greater or equal to 20140609
            if (version >= 20140609)
            {
                AmountOfPairsStandard = o.ReadInt32();
                PairsStandard = new List<IntDoublePair>();
                for (int i = 0; i < AmountOfPairsStandard; i++)
                {
                    PairsStandard.Add(o.ReadIntDoublePair());
                }
                AmountOfPairsTaiko = o.ReadInt32();
                PairsTaiko = new List<IntDoublePair>();
                for (int i = 0; i < AmountOfPairsTaiko; i++)
                {
                    PairsTaiko.Add(o.ReadIntDoublePair());
                }
                AmountOfPairsCTB = o.ReadInt32();
                PairsCTB = new List<IntDoublePair>();
                for (int i = 0; i < AmountOfPairsCTB; i++)
                {
                    PairsCTB.Add(o.ReadIntDoublePair());
                }
                AmountOfPairsMania = o.ReadInt32();
                PairsMania = new List<IntDoublePair>();
                for (int i = 0; i < AmountOfPairsMania; i++)
                {
                    PairsMania.Add(o.ReadIntDoublePair());
                } 
            }

            DrainTime = o.ReadInt32();
            TotalTime = o.ReadInt32();
            TimeOfPreview = o.ReadInt32();
            AmountOfTimingPoints = o.ReadInt32();
            TimingPoints = new List<Timingpoint>();
            for (int i = 0; i < AmountOfTimingPoints; i++)
            {
                TimingPoints.Add(o.ReadTimingpoint());
            }
            BeatmapID = o.ReadInt32();
            BeatmapSetID = o.ReadInt32();
            ThreadID = o.ReadInt32();
            GradeAchievedStandard = o.ReadByte();
            GradeAchievedTaiko = o.ReadByte();
            GradeAchievedCTB = o.ReadByte();
            GradeAchievedMania = o.ReadByte();
            LocalBeatmapOffset = o.ReadInt16();
            StackLeniency = o.ReadSingle();
            GameplayMode = o.ReadByte();
            SongSource = o.ReadString();
            SongTags = o.ReadString();
            OnlineOffset = o.ReadInt16();
            Font = o.ReadString();
            Unplayed = o.ReadBoolean();
            LastTimePlayed = o.ReadInt64();
            Osz2 = o.ReadBoolean();
            FolderName = o.ReadString();
            LastTimeChecked = o.ReadInt64();
            IgnoreBeatmapSound = o.ReadBoolean();
            IgnoreBeatmapSkin = o.ReadBoolean();
            DisableStoryboard = o.ReadBoolean();
            DisableVideo = o.ReadBoolean();
            VisualOverride = o.ReadBoolean();

            // unknown short, only present if version less than 20140609
            if (version < 20140609)
            {
                Unknown = o.ReadInt16();
            }

            LastModificationTime = o.ReadInt32();
            ManiaScrollSpeed = o.ReadByte();
        }

        public void WriteToStream(OsuWriter o)
        {
            o.Write(SizeInBytes);
            o.Write(ArtistName);
            o.Write(ArtistNameUnicode);
            o.Write(SongTitle);
            o.Write(SongTitleUnicode);
            o.Write(CreatorName);
            o.Write(Difficulty);
            o.Write(AudioFileName);
            o.Write(MD5Beatmap);
            o.Write(NameDotOsuFile);
            o.Write(RankedStatus);
            o.Write(NumberOfHitcircles);
            o.Write(NumberOfSliders);
            o.Write(NumberOfSpinners);
            o.Write(ModificationTime);

            // next 4 are bytes for versions smaller than 20140609
            if (Version >= 20140609)
            {
                o.Write(ApproacRate);
                o.Write(CircleSize);
                o.Write(HPDrain);
                o.Write(OverallDifficulty);
            }
            else
            {
                o.Write(ApproacRateB);
                o.Write(CircleSizeB);
                o.Write(HPDrainB);
                o.Write(OverallDifficultyB);
            }

            o.Write(SliderVelocity);

            // next part only present if version greater or equal to 20140609
            if (Version >= 20140609)
            {
                o.Write(AmountOfPairsStandard);
                foreach (var pair in PairsStandard)
                {
                    o.Write(pair);
                }
                o.Write(AmountOfPairsTaiko);
                foreach (var pair in PairsTaiko)
                {
                    o.Write(pair);
                }
                o.Write(AmountOfPairsCTB);
                foreach (var pair in PairsCTB)
                {
                    o.Write(pair);
                }
                o.Write(AmountOfPairsMania);
                foreach (var pair in PairsMania)
                {
                    o.Write(pair);
                }
            }

            o.Write(DrainTime);
            o.Write(TotalTime);
            o.Write(TimeOfPreview);
            o.Write(AmountOfTimingPoints);
            foreach (var tp in TimingPoints)
            {
                o.Write(tp);
            }
            o.Write(BeatmapID);
            o.Write(BeatmapSetID);
            o.Write(ThreadID);
            o.Write(GradeAchievedStandard);
            o.Write(GradeAchievedTaiko);
            o.Write(GradeAchievedCTB);
            o.Write(GradeAchievedMania);
            o.Write(LocalBeatmapOffset);
            o.Write(StackLeniency);
            o.Write(GameplayMode);
            o.Write(SongSource);
            o.Write(SongTags);
            o.Write(OnlineOffset);
            o.Write(Font);
            o.Write(Unplayed);
            o.Write(LastTimePlayed);
            o.Write(Osz2);
            o.Write(FolderName);
            o.Write(LastTimeChecked);
            o.Write(IgnoreBeatmapSound);
            o.Write(IgnoreBeatmapSkin);
            o.Write(DisableStoryboard);
            o.Write(DisableVideo);
            o.Write(VisualOverride);

            // unknown short, only present if version less than 20140609
            if (Version < 20140609)
            {
                o.Write(Unknown);
            }

            o.Write(LastModificationTime);
            o.Write(ManiaScrollSpeed);
        }
    }
}
