﻿using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    public class Timingpoint
    {
        public double BPM { get; set; }
        public double Offset { get; set; } // in miliseconds
        public bool NotInherited { get; set; } // false if Timingpoint is inherited

        public Timingpoint(double BPM, double offset, bool notInherited)
        {
            this.BPM = BPM;
            Offset = offset;
            NotInherited = notInherited;
        }
    }
}
