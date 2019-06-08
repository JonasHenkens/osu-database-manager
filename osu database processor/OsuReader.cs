using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace osu_database_processor
{
    class OsuReader : BinaryReader
    {
        public OsuReader(Stream input) : base(input) { }

        // ReadByte
        // short = ReadInt16
        // int = ReadInt32
        // long = ReadInt64
        // TODO ReadULEB128
        // ReadSingle
        // ReadDouble
        // ReadBoolean
        // TODO ReadString

        // TODO int-double pair
        // TODO Timing point
        // TODO DateTime

    }
}
