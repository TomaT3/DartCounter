using System;
using System.Collections.Generic;
using System.Text;

namespace DartCounter.Model
{
    public enum DartStatus
    {
        Ok,
        OverThrown,
        NoValidCheckOut,
        NoCheckOutPossible,
        CheckedOut
    }
}
