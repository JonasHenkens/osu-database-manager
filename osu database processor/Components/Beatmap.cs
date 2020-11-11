using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    public class Beatmap
    {
        public int SizeInBytes { get; set; } // size of this entry
        public string ArtistName { get; set; }
        public string ArtistNameUnicode { get; set; }
        public string SongTitle { get; set; }
        public string SongTitleUnicode { get; set; }
        public string CreatorName { get; set; }
        public string Difficulty { get; set; } // e.g. Hard, Insane, etc.
        public string AudioFileName { get; set; }
        public string MD5Beatmap { get; set; } // MD5 of beatmap
        public string NameDotOsuFile { get; set; } // name of .osu file
        public RankedStatus RankedStatus { get; set; } // 0 = unknown, 1 = unsubmitted, 2 = pending/wip/graveyard, 3 = unused, 4 = ranked, 5 = approved, 6 = qualified, 7 = loved
        public short NumberOfHitcircles { get; set; }
        public short NumberOfSliders { get; set; } // note: will be present in every mode
        public short NumberOfSpinners { get; set; } // note: will be present in every mode
        public DateTime ModificationTime { get; set; } // last modification time in windows ticks

        // next 4 are bytes for versions smaller than 20140609
        public float ApproacRate { get; set; }
        public float CircleSize { get; set; }
        public float HPDrain { get; set; }
        public float OverallDifficulty { get; set; }
        // for version smaller than 20140609
        public byte ApproacRateB { get; set; }
        public byte CircleSizeB { get; set; }
        public byte HPDrainB { get; set; }
        public byte OverallDifficultyB { get; set; }
        //

        public double SliderVelocity { get; set; }

        // next part only present if version greater or equal to 20140609
        // double is star rating
        public int AmountOfPairsStandard { get { return PairsStandard.Count; } }
        public List<ModsDoublePair> PairsStandard { get; private set; }
        public int AmountOfPairsTaiko { get { return PairsTaiko.Count; } }
        public List<ModsDoublePair> PairsTaiko { get; private set; }
        public int AmountOfPairsCTB { get { return PairsCTB.Count; } }
        public List<ModsDoublePair> PairsCTB { get; private set; }
        public int AmountOfPairsMania { get { return PairsMania.Count; } }
        public List<ModsDoublePair> PairsMania { get; private set; }
        //

        public int DrainTime { get; set; } // in seconds
        public int TotalTime { get; set; } // in miliseconds
        public int TimeOfPreview { get; set; } // in miliseconds, starting time of audiopreview
        public int AmountOfTimingPoints { get { return TimingPoints.Count; } }
        public List<Timingpoint> TimingPoints { get; private set; }
        public int BeatmapID { get; set; }
        public int BeatmapSetID { get; set; }
        public int ThreadID { get; set; }
        public Grade GradeAchievedStandard { get; set; }
        public Grade GradeAchievedTaiko { get; set; }
        public Grade GradeAchievedCTB { get; set; }
        public Grade GradeAchievedMania { get; set; }
        public short LocalBeatmapOffset { get; set; }
        public float StackLeniency { get; set; }
        public Mode GameplayMode { get; set; }
        public string SongSource { get; set; }
        public string SongTags { get; set; }
        public short OnlineOffset { get; set; }
        public string Font { get; set; }
        public bool Unplayed { get; set; }
        public long LastTimePlayed { get; set; }
        public bool Osz2 { get; set; } // is the beatmap osz2
        public string FolderName { get; set; } // relative to songs folder
        public long LastTimeChecked { get; set; } // last time the beatmap was checked against osu! repository
        public bool IgnoreBeatmapSound { get; set; }
        public bool IgnoreBeatmapSkin { get; set; }
        public bool DisableStoryboard { get; set; }
        public bool DisableVideo { get; set; }
        public bool VisualOverride { get; set; }

        public short Unknown { get; set; } // only present if version less than 20140609

        public int LastModificationTime { get; set; } // ?
        public byte ManiaScrollSpeed { get; set; }

        public int Version { get; set; }

        public Beatmap(OsuReader o, int version)
        {
            ReadFromStream(o, version);
        }

        public void ReadFromStream(OsuReader o, int version)
        {
            Version = version;
            if (version < 20191106)
            {
                SizeInBytes = o.ReadInt32();
            }
            ArtistName = o.ReadString();
            ArtistNameUnicode = o.ReadString();
            SongTitle = o.ReadString();
            SongTitleUnicode = o.ReadString();
            CreatorName = o.ReadString();
            Difficulty = o.ReadString();
            AudioFileName = o.ReadString();
            MD5Beatmap = o.ReadString();
            NameDotOsuFile = o.ReadString();
            RankedStatus = (RankedStatus)o.ReadByte();
            NumberOfHitcircles = o.ReadInt16();
            NumberOfSliders = o.ReadInt16();
            NumberOfSpinners = o.ReadInt16();
            ModificationTime = new DateTime(o.ReadInt64());

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
                int amountOfPairsStandard = o.ReadInt32();
                PairsStandard = new List<ModsDoublePair>();
                for (int i = 0; i < amountOfPairsStandard; i++)
                {
                    PairsStandard.Add(o.ReadIntDoublePair());
                }
                int amountOfPairsTaiko = o.ReadInt32();
                PairsTaiko = new List<ModsDoublePair>();
                for (int i = 0; i < amountOfPairsTaiko; i++)
                {
                    PairsTaiko.Add(o.ReadIntDoublePair());
                }
                int amountOfPairsCTB = o.ReadInt32();
                PairsCTB = new List<ModsDoublePair>();
                for (int i = 0; i < amountOfPairsCTB; i++)
                {
                    PairsCTB.Add(o.ReadIntDoublePair());
                }
                int amountOfPairsMania = o.ReadInt32();
                PairsMania = new List<ModsDoublePair>();
                for (int i = 0; i < amountOfPairsMania; i++)
                {
                    PairsMania.Add(o.ReadIntDoublePair());
                } 
            }

            DrainTime = o.ReadInt32();
            TotalTime = o.ReadInt32();
            TimeOfPreview = o.ReadInt32();
            int amountOfTimingPoints = o.ReadInt32();
            TimingPoints = new List<Timingpoint>();
            for (int i = 0; i < amountOfTimingPoints; i++)
            {
                TimingPoints.Add(o.ReadTimingpoint());
            }
            BeatmapID = o.ReadInt32();
            BeatmapSetID = o.ReadInt32();
            ThreadID = o.ReadInt32();
            GradeAchievedStandard = (Grade)o.ReadByte();
            GradeAchievedTaiko = (Grade)o.ReadByte();
            GradeAchievedCTB = (Grade)o.ReadByte();
            GradeAchievedMania = (Grade)o.ReadByte();
            LocalBeatmapOffset = o.ReadInt16();
            StackLeniency = o.ReadSingle();
            GameplayMode = (Mode)o.ReadByte();
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

            if (BeatmapSetID == 101796)
            {
                
            }
        }

        public void WriteToStream(OsuWriter o)
        {
            if (Version < 20191106)
            {
                o.Write(SizeInBytes);
            }
            o.Write(ArtistName);
            o.Write(ArtistNameUnicode);
            o.Write(SongTitle);
            o.Write(SongTitleUnicode);
            o.Write(CreatorName);
            o.Write(Difficulty);
            o.Write(AudioFileName);
            o.Write(MD5Beatmap);
            o.Write(NameDotOsuFile);
            o.Write((byte)RankedStatus);
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
            o.Write((byte)GradeAchievedStandard);
            o.Write((byte)GradeAchievedTaiko);
            o.Write((byte)GradeAchievedCTB);
            o.Write((byte)GradeAchievedMania);
            o.Write(LocalBeatmapOffset);
            o.Write(StackLeniency);
            o.Write((byte)GameplayMode);
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

        public Grade getGrade(Mode gameMode)
        {
            switch (gameMode)
            {
                case Mode.Standard:
                    return GradeAchievedStandard;
                case Mode.Taiko:
                    return GradeAchievedTaiko;
                case Mode.CTB:
                    return GradeAchievedCTB;
                case Mode.Mania:
                    return GradeAchievedMania;
                default:
                    return Grade.Unplayed;
            }
        }
    }
}
