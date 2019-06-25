using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    class IntDoublePair
    {
        public int Int { get; set; }
        public double Double { get; set; }

        public IntDoublePair(int intValue, double doubleValue)
        {
            Int = intValue;
            Double = doubleValue;
        }
    }
}
