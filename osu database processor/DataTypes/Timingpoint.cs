using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.DataTypes
{
    class Timingpoint
    {
        public double BPM { get; private set; }
        public double Offset { get; private set; } // in miliseconds
        public bool NotInherited { get; private set; } // false if Timingpoint is inherited

        public Timingpoint(double BPM, double offset, bool notInherited)
        {
            this.BPM = BPM;
            Offset = offset;
            NotInherited = notInherited;
        }
    }
}
