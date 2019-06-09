using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.DataTypes
{
    class IntDoublePair
    {
        public int Int { get; private set; }
        public double Double { get; private set; }

        public IntDoublePair(int intValue, double doubleValue)
        {
            Int = intValue;
            Double = doubleValue;
        }
    }
}
