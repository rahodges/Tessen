using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amns.Tessen
{
    public enum DojoMembershipDayType : byte
    {
        NotSet                  = 0x00,
        CurrentDay              = 0x10,
        
        FirstOfMonth            = 0x20,
        EndOfMonth              = 0x2A,
        EndOfFollowingMonth     = 0x2B,

        FirstOfYear             = 0x30,
        EndOfYear               = 0x3A,
        EndOfFollowingYear      = 0x3B
    }
}
