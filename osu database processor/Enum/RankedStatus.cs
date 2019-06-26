using System;
using System.Collections.Generic;
using System.Text;

namespace osu_database_processor.Enum
{
    public enum RankedStatus
    {
        Unknown,
        Unsubmitted,
        pendingWipGraveyard,
        Unused,
        Ranked,
        Approved,
        Qualified,
        Loved
    }
}
