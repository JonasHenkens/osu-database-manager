using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Components
{
    class ModsDoublePair
    {
        public Mods Mods { get; set; }
        public double Double { get; set; }

        public ModsDoublePair(Mods mods, double doubleValue)
        {
            Mods = mods;
            Double = doubleValue;
        }
    }
}
