using System;
using System.Collections.Generic;
using System.Text;

namespace osu_api_wrapper
{
    public enum Mode
    {   // 0x00 = osu!Standard, 0x01 = Taiko, 0x02 = CTB, 0x03 = Mania
        Standard,
        Taiko,
        CTB,
        Mania
    }

    public enum RankedStatus
    {
        Unknown,
        Unsubmitted,
        PendingWipGraveyard,
        Unused,
        Ranked,
        Approved,
        Qualified,
        Loved
    }

    public enum ScoreRank
    {
        XH, //SS+
        SH, //S+
        X,  //SS
        S,
        A,
        B,
        C,
        D,
        F,
        Unplayed
    }

    [Flags]
    public enum Mods
    {
        None = 0,
        NoFail = 1,
        Easy = 2,
        TouchDevice = 4,
        Hidden = 8,
        HardRock = 16,
        SuddenDeath = 32,
        DoubleTime = 64,
        Relax = 128,
        HalfTime = 256,
        Nightcore = 512, // Only set along with DoubleTime. i.e: NC only gives 576
        Flashlight = 1024,
        Autoplay = 2048,
        SpunOut = 4096,
        Relax2 = 8192,    // Autopilot
        Perfect = 16384, // Only set along with SuddenDeath. i.e: PF only gives 16416  
        Key4 = 32768,
        Key5 = 65536,
        Key6 = 131072,
        Key7 = 262144,
        Key8 = 524288,
        FadeIn = 1048576,
        Random = 2097152,
        Cinema = 4194304,
        Target = 8388608,
        Key9 = 16777216,
        KeyCoop = 33554432,
        Key1 = 67108864,
        Key3 = 134217728,
        Key2 = 268435456,
        ScoreV2 = 536870912,
        LastMod = 1073741824,
        KeyMod = Key1 | Key2 | Key3 | Key4 | Key5 | Key6 | Key7 | Key8 | Key9 | KeyCoop,
        FreeModAllowed = NoFail | Easy | Hidden | HardRock | SuddenDeath | Flashlight | FadeIn | Relax | Relax2 | SpunOut | KeyMod,
        ScoreIncreaseMods = Hidden | HardRock | DoubleTime | Flashlight | FadeIn
    }

    public enum AddMode
    {
        Skip,
        Merge,
        Overwrite
    }

    public enum Genre
    { // 0 = any, 1 = unspecified, 2 = video game, 3 = anime, 4 = rock, 5 = pop, 6 = other, 7 = novelty, 9 = hip hop, 10 = electronic (note that there's no 8)
        Any,
        Unspecified,
        VideoGame,
        Anime,
        Rock,
        Pop,
        Other,
        Novelty,
        Unused,
        HipHop,
        Electronic
    }

    public enum Language
    { // 0 = any, 1 = other, 2 = english, 3 = japanese, 4 = chinese, 5 = instrumental, 6 = korean, 7 = french, 8 = german, 9 = swedish, 10 = spanish, 11 = italian
        Any,
        Other,
        English,
        Japanese,
        Chinese,
        Instrumental,
        Korean,
        French,
        German,
        Swedish,
        Spanish,
        Italian
    }

    public enum RankedStatusAPI
    { // 4 = loved, 3 = qualified, 2 = approved, 1 = ranked, 0 = pending, -1 = WIP, -2 = graveyard
        Graveyard = -2,
        WIP = -1,
        Pending = 0,
        Ranked = 1,
        Approved = 2,
        Qualified = 3,
        Loved = 4
    }

    public enum WinningCondition
    { // winning condition: score = 0, accuracy = 1, combo = 2, score v2 = 3
        Score,
        Accuracy,
        Combo,
        ScoreV2
    }

    public enum TeamType
    { // Head to head = 0, Tag Co-op = 1, Team vs = 2, Tag Team vs = 3
        HeadToHead,
        TagCoOp,
        TeamVS,
        TagTeamVS
    }

    public enum UsernameType
    {
        String,
        Id
    }
}
