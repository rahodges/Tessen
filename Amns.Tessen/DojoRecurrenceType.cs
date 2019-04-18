/* ****************************************************** 
 * Amns.Tessen
 * Copyright © 2004 Roy A.E. Hodges. All Rights Reserved.
 * ****************************************************** */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amns.Tessen
{
    public enum DojoRecurrenceType : byte
    {
        DurationReserved = 0,
        Daily = 10,
        Weekly = 11,
        Monthly = 12,
        Yearly = 13,
        Duration = 30
    }
}
