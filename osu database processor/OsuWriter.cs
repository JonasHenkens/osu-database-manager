﻿using osu_database_processor.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace osu_database_processor
{
    class OsuWriter : BinaryWriter
    {
        public OsuWriter(Stream output) : base(output) { }

        public override void Write(string value)
        {
            if (value == null)
            {
                Write(new byte());
            }
            else
            {
                Write((byte)0x0b);
                base.Write(value);
            }
        }

        public void Write(IntDoublePair value)
        {
            Write((byte)0x08);
            Write(value.Int);
            Write((byte)0x08);
            Write(value.Double);
        }

        public void Write(Timingpoint value)
        {
            Write(value.BPM);
            Write(value.Offset);
            Write(value.NotInherited);
        }

        public void Write(DateTime value)
        {
            Write(value.Ticks);
        }
    }
}